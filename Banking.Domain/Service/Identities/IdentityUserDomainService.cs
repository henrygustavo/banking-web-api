namespace Banking.Domain.Service.Identities
{
    using System;
    using Application.Notification;
    using Banking.Domain.Entity.Identities;

    public class IdentityUserDomainService: IIdentityUserDomainService
    {

        public void PerformNewUser(IdentityUser newIdentityUser,
                                   IdentityUser identityUserWithSameEmail,
                                   IdentityUser identityUserWithSameUserName)
        {

            Notification notification = Validation(identityUserWithSameEmail, identityUserWithSameUserName);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }
        }

        public string PerformAuthentication(IdentityUser identityUser,
                                            string loginUserName, string loginPassword,
                                            string jwKey, string jwIssuer)
        {
            Notification notification = ValidateAuthentication(identityUser, loginUserName, loginPassword);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            return identityUser.BuildToken(jwKey, jwIssuer);
        }

        private Notification ValidateAuthentication(IdentityUser identityUser, string loginUserName,
                                                    string loginPassword)
        {
            Notification notification = new Notification();

            if (identityUser == null)
            {
                notification.AddError("Your account doesn't exists");
                return notification;
            }

            if (!identityUser.HasValidCredentials(loginUserName, loginPassword))
            {
                notification.AddError("Your credentials are not correct");
                return notification;
            }

            if (!identityUser.Active)
            {
                notification.AddError("Your account is not actived, please reach out the web master.");
                return notification;
            }

            return notification;
        }

        private Notification Validation(IdentityUser identityUserWithSameEmail,
                                        IdentityUser identityUserWithSameUserName)
        {
            Notification notification = new Notification();
            this.ValidateUserName(notification, identityUserWithSameUserName);
            this.ValidateEmail(notification, identityUserWithSameEmail);
            
            return notification;
        }

        private void ValidateEmail(Notification notification,
                                   IdentityUser identityUserWithSameEmail)
        {
            if (identityUserWithSameEmail != null)
            {
                notification.AddError("That email already exists.");
            }
        }
        private void ValidateUserName(Notification notification,
                                      IdentityUser identityUserWithSameUserName)
        {
            if (identityUserWithSameUserName != null)
            {
                notification.AddError("That user Name already exists.");
            }

        }
    }
}
