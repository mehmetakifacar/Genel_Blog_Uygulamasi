﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetCategoryList();
        void CategoryAdd(Category category);
        void CategoryDelete(Category category);
        void CategoryUpdate(Category category);
        Category GetById(int id);

    }
}
