using EndPoint.Site.Convertors;
using EndPoint.Site.ViewModels.SiteViewModels.Page;
using ManchesterFans.Application.FacadPatterns;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EndPoint.Site.ViewComponents
{
    public class SliderInHome : ViewComponent
    {
        private readonly IPageFacad _pageFacad;
        public SliderInHome(IPageFacad pageFacad)
        {
            _pageFacad = pageFacad;
        }
        public IViewComponentResult Invoke()
        {

            var result = _pageFacad.GetPagesForSiteSliderService.Execute();

            return View("SliderInHome",result.Data
                .Select(p=> new PagesInSliderViewModel()
                {
                    Date = p.Date.ToShamsi(),
                    GroupName = p.GroupName,
                    ImageName= p.ImageName,
                    Likes = p.Likes,
                    Title = p.Title,
                    Visits = p.Visits
                }));
        }
    }
}
