using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OrdersManagement.Core.Entities;

namespace OrdersManagement.Core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity


	{
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();


		public BaseSpecifications()
		{

			
		
		}


		public BaseSpecifications(Expression<Func<T, bool>> CriteriaExpression)
		{
			Criteria = CriteriaExpression;     // P => P.Id == 10

		}


		


	}
}
