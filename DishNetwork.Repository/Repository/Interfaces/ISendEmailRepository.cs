using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Repository.Repository.Interfaces
{
	public interface ISendEmailRepository
	{
		public bool SendEmail(String To, string Subject, string Body);
	}
}
