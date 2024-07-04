using SkelandStore.Core.Entities;
using System.Linq.Expressions;

namespace SkelandStore.Core.Specification
{
    public class BaseSpacifictions<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteris { get; set; }//Automatic Prop
        public List<Expression<Func<T, object>>> InCludes { get; set; } = new List<Expression<Func<T, object>>>(); //instead of initilise object in 2 Constructors ,initilise it with prop
        public Expression<Func<T, object>> Orderby { get; set; }//To store Value Of Order
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Take = take;
            Skip = skip;
        }

        //Used in Get All 
        public BaseSpacifictions()
        {//InCludes = new List<Expression<Func<T, object>>>();//declare an Object in constructor to can use it 
         // InCludes.Add(p => p.ProductBrand); => can't add expression of include of Product cause this class is generic so we create another class to be more specific type 
        }

        //Used In Get By Id
        public BaseSpacifictions(Expression<Func<T, bool>> CretriaExpression)
        {
            Criteris = CretriaExpression;
            //InCludes=new List<Expression<Func<T, object>>>();
        }


        //do 2 Functions to set Value Of Order and not assign value with Constructor cause not all Requests require Order
        //1)function to set value of OrderByAsce
        public void AddOrderBy(Expression<Func<T, object>> OrderbyExpression)
        {
            Orderby = OrderbyExpression;
        }

        //2)function To set value of OrderByDesc
        public void AddOrderByDesc(Expression<Func<T, object>> OrderbyDescExpression)
        {
            OrderByDesc = OrderbyDescExpression;
        }
    }
}
