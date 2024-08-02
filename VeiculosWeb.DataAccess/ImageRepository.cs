﻿using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class ImageRepository(VeiculosWebContext context) : BaseRepository<Image, VeiculosWebContext>(context), IImageRepository
    {
    }
}
