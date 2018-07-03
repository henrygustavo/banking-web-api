namespace Banking.Domain.Service.Customers
{
    using System;
    using Application.Notification;
    using Banking.Domain.Entity.Customers;
    using Banking.Domain.Entity.Identities;

    public class CustomerDomainService: ICustomerDomainService
    {

        public void PerformNewCustomer(Customer customer, Customer searchedCustomerByDni, int identityUserId)
        {
            Notification notification = ValidationInsert(customer, searchedCustomerByDni);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }
            customer.IdentityUserId = identityUserId;
        }

        public void PerformUpdateCustomer(Customer customer, string firstName,
                                         string lastName, bool active, IdentityUser identityUser)
        {
            Notification notification = ValidationUpdate(customer, identityUser);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Active = active;
            customer.IdentityUser = identityUser;
            customer.IdentityUser.Active = active;

        }

        private Notification ValidationInsert(Customer customer, Customer searchedCustomerByDni)
        {
            Notification notification = new Notification();
            this.ValidateDni(notification, customer, searchedCustomerByDni);
            
            return notification;
        }

        private Notification ValidationUpdate(Customer customer,IdentityUser identityUser)
        {
            Notification notification = new Notification();
            this.ValidateIdentityUser(notification, identityUser);
            this.ValidateId(notification, customer);

            return notification;
        }

        private void ValidateDni(Notification notification, Customer customer, Customer searchedCustomerByDni)
        {
            if ( customer.Dni == searchedCustomerByDni.Dni)
            {
                notification.AddError("That dni already exists.");
            }
        }

        private void ValidateIdentityUser(Notification notification, IdentityUser identityUser)
        {
            if (identityUser == null)
            {
                notification.AddError("A user account must be attached to this customer.");
            }
        }

        private void ValidateId(Notification notification, Customer customer)
        {
            if (customer.Id == 0)
            {
                notification.AddError("This customer does not have an id");
            }
        }
    }
}
