using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DMJ.DIRS21.DataAccess.Repository;
using DMJ.DIRS21.Model;
using DMJ.DIRS21.Model.ClassVm;
using DMJ.DIRS21.Model.Documents;
using DMJ.DIRS21.Model.SearchVms;
using DMJ.DIRS21.Service.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DMJ.DIRS21.Service.Services
{
    public class DishChangeService : IDishChangeService
    {
        private readonly IRepository _repository;
        public DishChangeService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task InsertDishAsync(Dish dish)
        {
            await _repository.InsertOneAsync(dish);
        }
        
        public async Task UpdateDishAsync(Dish dish)
        {
            await _repository.UpdateOneAsync(dish,c=>c.Code == dish.Code);
        }
        
        public async Task<long> DeleteDishAsync(Dish dish)
        {
            return await _repository.DeleteAsync<Dish>(c => c.Code == dish.Code);
        }
        
        public async Task<long> DeleteDishByCodeAsync(int code)
        {
            return await _repository.DeleteAsync<Dish>(c=>c.Code == code);
        }

        public async Task AddCommentsAsync(DishCommentVm commentVm)
        {
            var selectedDish = await _repository.SingleAsync<Dish>(c => c.Code == commentVm.DishCode);

            commentVm.Comments.ForEach(comment=>comment.InsertionDate = DateTime.Now);

            if (selectedDish != null)
            {
                selectedDish.DishComments ??= new List<DishComment>();

                selectedDish.DishComments.AddRange(commentVm.Comments);

                await _repository.UpdateOneAsync<Dish>(selectedDish,c=>c.Code == commentVm.DishCode);
            }
        }

        public async Task AddScoreAsync(DishScoreVm scoreVm)
        {
            var selectedDish = await _repository.SingleAsync<Dish>(c => c.Code == scoreVm.DishCode);

            if (selectedDish != null)
            {
                selectedDish.Score = +scoreVm.Score;

                await _repository.UpdateOneAsync<Dish>(selectedDish, c => c.Code == scoreVm.DishCode);
            }
        }

        public async Task DeactivateDishAsync(DishDeactivationVm deactivationVm)
        {
            var selectedDish = await _repository.SingleAsync<Dish>(c => c.Code == deactivationVm.DishCode);

            if (selectedDish != null)
            {
                selectedDish.IsActive = false;
                
                selectedDish.DeactivationReason = deactivationVm.DeactivationReason;

                await _repository.UpdateOneAsync<Dish>(selectedDish, c => c.Code == deactivationVm.DishCode);
            }
        }

        public async Task ActivateDishAsync(int dishCode)
        {
            var selectedDish = await _repository.SingleAsync<Dish>(c => c.Code == dishCode);

            if (selectedDish != null)
            {
                selectedDish.IsActive = true;

                selectedDish.DeactivationReason = string.Empty;

                await _repository.UpdateOneAsync<Dish>(selectedDish, c => c.Code == dishCode);
            }
        }
    }
}
