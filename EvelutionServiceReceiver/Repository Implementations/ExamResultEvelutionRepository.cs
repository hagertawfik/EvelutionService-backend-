using EvelutionServiceReceiver.Data_Models;
using EvelutionServiceReceiver.DataContextEvelution;
using EvelutionServiceReceiver.Repository_Interfaces;

namespace EvelutionServiceReceiver.Repository_Implementations
{
    public class ExamResultEvelutionRepository : IExamResultEvelutionRepository
    {
        private readonly DataContextEvelution1 _evelutioncontext;

        public ExamResultEvelutionRepository( DataContextEvelution1 evelutioncontext)
        {
            
            _evelutioncontext = evelutioncontext;
            
        }
       

        public void AddExamResult(ExamResult examResult)
        {
            _evelutioncontext.ExamResults.Add(examResult);
            _evelutioncontext.SaveChanges();
        }
       
    }
}
