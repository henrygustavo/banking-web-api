namespace Banking.Domain.Test
{
    using Banking.Domain.Service.Transactions;
    using System;
    using Xunit;

    public class TransferDomainServiceTest
    {
        [Fact]

        public void TestPerformTransfer()
        {
            TransferDomainService transferDomainService = new TransferDomainService();

            Assert.Throws<ArgumentException>(() => transferDomainService.PerformTransfer(null, null, 0));
        }
    }
}
