namespace Banking.Domain.Service.Identities
{
    using System;
    using Application.Notification;
    using Banking.Domain.Entity.Identities;

    public class IdentityUserDomainService: IIdentityUserDomainService
    {

        public IdentityUser PerformNewUser(string userName, string email, string password,
                                           bool active, IdentityUser searchedIdentityUserByEmail,
                                           IdentityUser searchedIdentityUserByUserName)
        {

            Notification notification = Validation(userName, email, searchedIdentityUserByEmail,
                                                                    searchedIdentityUserByUserName);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

           return new IdentityUser(userName, email, password, active, "member");
        
        }

        private Notification Validation(string userName, string email, IdentityUser searchedIdentityUserByEmail,
                                                                      IdentityUser searchedIdentityUserByUserName)
        {
            Notification notification = new Notification();
            this.ValidateUserName(notification, userName, searchedIdentityUserByUserName);
            this.ValidateEmail(notification, email, searchedIdentityUserByEmail);
            
            return notification;
        }

        private void ValidateEmail(Notification notification, string email,
                                   IdentityUser searchedIdentityUserByEmail)
        {
            if (email == searchedIdentityUserByEmail.Email)
            {
                notification.AddError("That email already exists.");
            }
        }
        private void ValidateUserName(Notification notification, string userName,
                                      IdentityUser searchedIdentityUserByUserName)
        {
            if (userName == searchedIdentityUserByUserName.UserName)
            {
                notification.AddError("That user Name already exists.");
            }

        }
    }
}
