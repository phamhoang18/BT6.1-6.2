using System.Collections.Generic;
using HRM_Payroll_System.DAL;
using HRM_Payroll_System.DTO;

namespace HRM_Payroll_System.BUS
{
    public class PhongBanBUS
    {
        private readonly PhongBanDAL _dal = new PhongBanDAL();
        public List<PhongBanDTO> GetAll() => _dal.GetAll();
    }
}