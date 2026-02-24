using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Application.Dtos;
using TechSouq.Domain.Entities;
using TechSouq.Domain.Interfaces;

namespace TechSouq.Application.Services
{
    public class CategorieService
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategorieService> _logger;

        public CategorieService(ICategorieRepository cartItemRepository,IMapper mapper,ILogger<CategorieService> logger)
        {
            _categorieRepository = cartItemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        async Task<OperationResult<int>> CreateCategorie(CategorieDto categorieDto)
        {
            try
            {
                var Categorie = _mapper.Map<Categorie>(categorieDto);

                var newId = await _categorieRepository.AddCategorie(Categorie);

                _logger.LogInformation("Create Categorie : {Id} Successfully", newId);
                return OperationResult<int>.Success(newId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Create Categorie Failed");
                return OperationResult<int>.Failure("Create Categorie Failed Try Later");
            }
        }

        async Task<OperationResult<Categorie>> GetCategorie(int categorieId)
        {
            try
            {
                if(categorieId<=0)
                {
                    _logger.LogWarning("Invalid data categorie Id: {Id}", categorieId);
                    return OperationResult<Categorie>.BadRequest($"Invalid data", new List<string> { $"Invalid data categorie Id: {categorieId}" });
                }

                var Categorie = await _categorieRepository.GetCategorie(categorieId);

                if(Categorie == null)
                {
                    _logger.LogError("categorie Id: {Id} Get Failed", categorieId);
                    return OperationResult<Categorie>.Failure("Get Categorie Failed Try Later");
                }

                _logger.LogInformation("categorie Id: {Id} Get Successfully", categorieId);
                return OperationResult<Categorie>.Success(Categorie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "categorie Id: {Id} Get Failed", categorieId);
                return OperationResult<Categorie>.Failure("Get Categorie Failed Try Later");
            }
        }

    }
}
