using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BusinessObject.Enums;
using UnidecodeSharpFork;
using Repository.Implements;

namespace IDBMS_API.Services
{
    public class TaskDesignService
    {
        private readonly ITaskDesignRepository _taskDesignRepo;
        private readonly ITaskCategoryRepository _taskCategoryRepo;
        public TaskDesignService(ITaskDesignRepository taskDesignRepo, ITaskCategoryRepository taskCategoryRepo)
        {
            _taskDesignRepo = taskDesignRepo;
            _taskCategoryRepo = taskCategoryRepo;
        }

        public IEnumerable<TaskDesign> Filter(IEnumerable<TaskDesign> list,
            string? codeOrName, int? taskCategoryId)
        {
            IEnumerable<TaskDesign> filteredList = list;

            if (codeOrName != null)
            {
                filteredList = filteredList.Where(item =>
                            (item.Code != null && item.Code.Unidecode().IndexOf(codeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Name != null && item.Name.Unidecode().IndexOf(codeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (taskCategoryId != null)
            {
                filteredList = filteredList.Where(item => item.TaskCategoryId == taskCategoryId);
            }

            return filteredList;
        }

        public IEnumerable<TaskDesign> GetAll(string? codeOrName, int? taskCategoryId)
        {
            var list = _taskDesignRepo.GetAll();

            return Filter(list, codeOrName, taskCategoryId);
        }
        public TaskDesign? GetById(int id)
        {
            return _taskDesignRepo.GetById(id) ?? throw new Exception("This task design id is not existed!");
        }

        public bool CheckCodeExisted(string code)
        {
            return _taskDesignRepo.CheckCodeExisted(code);
        }

        public string GenerateCode(int? categoryId)
        {
            string code = String.Empty;
            Random random = new();

            for (int attempt = 0; attempt < 10; attempt++)
            {
                // Generate the code
                code = GenerateSingleCode(categoryId, random);


                bool codeExistsInTaskDesign = CheckCodeExisted(code);

                if (codeExistsInTaskDesign == false)
                {
                    return code;
                }
            }

            throw new Exception("Failed to generate a unique code after 10 attempts.");
        }

        public string GenerateSingleCode(int? categoryId, Random random)
        {
            string code = String.Empty;

            if (categoryId == null)
            {
                code += "KPL_KPL_";
            }
            else
            {
                TaskCategoryService taskCategoryService = new (_taskCategoryRepo);
                var category = taskCategoryService.GetById(categoryId.Value) ?? throw new Exception("This task category id is not existed!");
                var type = category.ProjectType;

                if (type == ProjectType.Decor)
                {
                    code += "TK_";
                }
                if (type == ProjectType.Construction)
                {
                    code += "XD_";
                }

                var valid = category.Name.Contains(' ');
                if (valid)
                {
                    category.Name.Split(' ').ToList().ForEach(i => code += i[0].ToString().Unidecode().ToUpper());
                    code = code.Replace("/", "");
                    code += "_";
                }
                else
                {
                    code += category.Name.Substring(0, 2).Unidecode().ToUpper() + "_";
                }
            }

            code += random.Next(100000, 999999);

            return code;
        }

        public TaskDesign? CreateTaskDesign(TaskDesignRequest request)
        {
            var generateCode = GenerateCode(request.TaskCategoryId);

            var ctd = new TaskDesign
            {
                Code = generateCode,
                Name = request.Name,
                EnglishName = request.EnglishName,
                Description = request.Description,
                EnglishDescription = request.EnglishDescription,
                CalculationUnit = request.CalculationUnit,
                EstimatePricePerUnit = request.EstimatePricePerUnit,
                IsDeleted = false,
                TaskCategoryId = request.TaskCategoryId,
            };

            var ctdCreated = _taskDesignRepo.Save(ctd);
            return ctdCreated;
        }
        public void UpdateTaskDesign(int id, TaskDesignRequest request)
        {
            var ctd = _taskDesignRepo.GetById(id) ?? throw new Exception("This task design id is not existed!");

            ctd.Name = request.Name;
            ctd.EnglishName = request.EnglishName;
            ctd.Description = request.Description;
            ctd.EnglishDescription = request.EnglishDescription;
            ctd.CalculationUnit = request.CalculationUnit;
            ctd.EstimatePricePerUnit = request.EstimatePricePerUnit;
            ctd.TaskCategoryId = request.TaskCategoryId;

            _taskDesignRepo.Update(ctd);
        }
        public void DeleteTaskDesign(int id)
        {
            var ctd = _taskDesignRepo.GetById(id) ?? throw new Exception("This task design id is not existed!");

            ctd.IsDeleted = true;

            _taskDesignRepo.Update(ctd);
        }
    }

}
