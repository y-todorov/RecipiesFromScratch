using System;
using System.ComponentModel.DataAnnotations;
using RecipiesMVC.DataAnnotations;
using RecipiesModelNS;

namespace RecipiesMVC.Models
{
    public class RecipeWasteViewModel : WasteViewModel
    {
        [Relation(EntityType = typeof(Recipe), DataFieldValue = "RecipeId", DataFieldText = "Name")]
        [Display(Name = "Recipe")]
        public int? RecipeId { get; set; }

        public static RecipeWasteViewModel ConvertFromRecipeWasteEntity(RecipeWaste entity,
            RecipeWasteViewModel model)
        {
            // TODO
            //ConvertFromWasteEntity(entity, model);
            model.RecipeId = entity.RecipeId;

            return model;
        }

        public static RecipeWaste ConvertToRecipeWasteEntity(RecipeWasteViewModel model,
            RecipeWaste entity)
        {
            // TODO

            //ConvertToWasteEntity(model, entity);
            entity.RecipeId = model.RecipeId;

            return entity;
        }
    }
}