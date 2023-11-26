using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvelutionServiceReceiver.Repository_Interfaces
{
    public interface ISubmitExamRepository
    {
        int GetCorrectChoiceId(int questionId);
    }
}
