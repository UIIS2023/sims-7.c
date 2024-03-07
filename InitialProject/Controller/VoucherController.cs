using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class VoucherController
    {

        private readonly VoucherService _voucherService;

        public VoucherController()
        {
            _voucherService = new VoucherService();
        }

        public List<Voucher> GetAll()
        {
            return _voucherService.GetAll();
        }

        public Voucher Get(int id)
        {

            return _voucherService.Get(id);

        }
        public Voucher Save(Voucher voucher)
        {

            return _voucherService.Save(voucher);
        }

        public void Delete(Voucher voucher)
        {
            _voucherService.Delete(voucher);
        }

        public Voucher Update(Voucher voucher)
        {
            return _voucherService.Update(voucher);
        }

        public List<Voucher> GetVouchersThatDidntExpire()
        {
            return _voucherService.GetVouchersThatDidntExpire();
        }

        public List<Voucher> GetVouchersThatArentUsed(List<Voucher> vouchers)
        {
            return _voucherService.GetVouchersThatArentUsed(vouchers);
        }

        public List<Voucher> VoucherForUser(User user)
        {
            return _voucherService.VoucherForUser(user);
        }
    }
}
