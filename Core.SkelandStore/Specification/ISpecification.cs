using SkelandStore.Core.Entities;
using System.Linq.Expressions;

namespace SkelandStore.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity//that make dealition only with realy tables in database
    {
        //this interface have signitures for Prop of parts of Query
        //1)Sign For Where Condition 
        //Where(P=>P.Id==Id) => Lambda Expression< Func delegate> : Func delegate Return Booliean 
        //lambda Expression <Func Delegate> Criteria{get;set;}
        public Expression<Func<T, bool>> Criteris { get; set; }//this will be Expression will take spacific Type of entity and return True or False

        //2)Sign For List Of Includes
        public List<Expression<Func<T, object>>> InCludes { get; set; }//this Expression will take spacific Type of entity and return specific Object From this entity

        //3)prop For OrderbyAsc[Orderby(p=>p.name)] 
        public Expression<Func<T, object>> Orderby { get; set; }
        //3)prop For Orderbydesc[Orderby(p=>p.name)]
        public Expression<Func<T, object>> OrderByDesc { get; set; }

        //4)Prop For Take 
        public int Take { get; set; }

        //5)Prop For Skip
        public int Skip { get; set; }

        public bool IsPaginationEnabled { get; set; }
    }
}
