using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";


        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
        }
      

        public List<Voucher> GetAll()
        {
            return _vouchers;
        }

        public int NextId()
        {
            
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(t => t.Id) + 1;
        }

        public Voucher Get(int id)
        {

            return _vouchers.Find(x => x.Id == id);

        }
        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;

        }

        public void Delete(Voucher voucher)
        {
            Voucher founded = _vouchers.Find(t => t.Id == voucher.Id);
            _vouchers.Remove(founded);
            _serializer.ToCSV(FilePath, _vouchers);
        }


        public Voucher Update(Voucher voucher)
        {
            
            Voucher current = _vouchers.Find(v => v.Id == voucher.Id);
            int index = _vouchers.IndexOf(current);
            _vouchers.Remove(current);
            _vouchers.Insert(index, voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public void BindVoucherUser()
        {
            foreach (Voucher voucher in _vouchers)
            {
                int userId = voucher.User.Id;
                User user = UserRepository.GetInstance().Get(userId);
                if (user != null)
                {
                    voucher.User = user;
                   
                }
                else
                {
                    Console.WriteLine("Error in VoucherUser binding");
                }
            }
        }
    }
}
