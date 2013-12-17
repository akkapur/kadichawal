using Interfaces.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAC.CRUD
{
    public interface IReviewCRUD<T> : IBaseCRUD<T>, ICRUDInitializer where T : Review, new()
    {
    }

    public class ReviewCRUD<T> : EntityCRUD<T>, IReviewCRUD<T> where T : Review, new()
    {
        public ReviewCRUD()
            : base(typeof(Review))
        {
        }
    }
}
