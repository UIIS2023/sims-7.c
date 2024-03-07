using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        Voucher Get(int id);
        List<Voucher> GetAll();

        Voucher Save(Voucher voucher);

        void Delete(Voucher voucher);

        Voucher Update(Voucher voucher);

        int NextId();
    }
}
