using AutoMapper;

using JdrTracker.Application.Campaigns.Common;
using JdrTracker.Application.Common.Interfaces;
using JdrTracker.Domain.Entities;

using MediatR;

namespace JdrTracker.Application.Campaigns.Commands.CreateCampaign;

//Handler : contient la logique du cas d'usage "créer une campagne"
//MediatR le trouve automatiquement grâce au typage IRequestHandler
public class CreateCampaignHandler : IRequestHandler<CreateCampaignCommand, CampaignDto>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    //Les dépendances sont injectées par le conteneur, le handler ne les crée jamais lui-mêmes
    public CreateCampaignHandler(
        ICampaignRepository campaignRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _campaignRepository = campaignRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //Point d'entrée appelé par MediatR : orchestre la création d'une campagne
    //Le Domain applique les règles, le repository prépare l'insertion, l'unit of work valide
    public async Task<CampaignDto> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        //Le Domain crée la campagne et applique ses règles (créateur = MJ donc le role sera MJ lors de la création)
        var campaign = Campaign.Create(request.Name, request.UserId, request.Nickname);

        //Préparation de l'insertion, puis validation en une seule transaction
        await _campaignRepository.AddAsync(campaign, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //Conversion de l'entité vers le DTO renvoyé au client
        return _mapper.Map<CampaignDto>(campaign);
    }
}