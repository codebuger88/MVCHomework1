using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MyAccounts.Helpers
{
    public static class PagerHelper
    {
        #region MvcPager

        private static string currentPageName = "page";

        public static IHtmlString MvcPager(this HtmlHelper html, int pageSize, int totalCount)
        {
            return MvcPager(html, pageSize, totalCount, false);
        }

        public static IHtmlString MvcPager(this HtmlHelper html, int pageSize, int totalCount, bool showNumberLink, string actionName = "", string controllerName = "")
        {
            var queryString = html.ViewContext.HttpContext.Request.QueryString;

            //當前頁
            int currentPage = 1;

            //總頁數
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1);

            //從ViewContext.RouteData.Values取得目前的RouteValueDictionary
            var routeValueDict = new System.Web.Routing.RouteValueDictionary(html.ViewContext.RouteData.Values);

            if (!string.IsNullOrEmpty(actionName))
                routeValueDict["action"] = actionName;
            if (!string.IsNullOrEmpty(controllerName))
                routeValueDict["controller"] = controllerName;

            var _renderPager = new StringBuilder();

            if (!string.IsNullOrEmpty(queryString[currentPageName]))
            {
                //與相應的QueryString綁定
                foreach (string key in queryString.Keys)
                {
                    if (queryString[key] != null && !string.IsNullOrEmpty(key))
                    {
                        routeValueDict[key] = queryString[key];
                    }
                }
                int.TryParse(queryString[currentPageName], out currentPage);
            }
            else
            {
                if (routeValueDict.ContainsKey(currentPageName))
                {
                    int.TryParse(routeValueDict[currentPageName].ToString(), out currentPage);
                }
            }

            //保留查詢字元到下一頁
            foreach (string key in queryString.Keys)
            {
                routeValueDict[key] = queryString[key];
            }

            if (currentPage <= 0)
            {
                currentPage = 1;
            }

            string classNameDisabled = "disabled";
            string classNameCurrent = "current";
            string firstPage = "<<";
            string lastPage = ">>";
            string prevPage = "prev";
            string nextPage = "next";
            string linkDisabledFormat = "<span class=\"" + classNameDisabled + "\">{0}</span>";
            string linkCurrentFormat = "<span class=\"" + classNameCurrent + "\">{0}</span>";
            string linkFormat = "<a>{0}</a>";

            if (totalPages > 1)
            {
                if (currentPage != 1)
                {
                    //處理首頁連結
                    routeValueDict[currentPageName] = 1;
                    _renderPager.AppendFormat(linkFormat, html.RouteLink(firstPage, routeValueDict));
                }
                else
                {
                    _renderPager.AppendFormat(linkDisabledFormat, firstPage);
                }

                if (currentPage > 1)
                {
                    //處理上一頁的連結
                    routeValueDict[currentPageName] = currentPage - 1;
                    _renderPager.AppendFormat(linkFormat, html.RouteLink(prevPage, routeValueDict));
                }
                else
                {
                    _renderPager.AppendFormat(linkDisabledFormat, prevPage);
                }

                #region 顯示頁數連結

                if (showNumberLink)
                {
                    var pageCount = (int) Math.Ceiling(totalCount/(double) pageSize);
                    const int nrOfPagesToDisplay = 10;

                    var start = 1;
                    var end = pageCount;

                    if (pageCount > nrOfPagesToDisplay)
                    {
                        var middle = (int) Math.Ceiling(nrOfPagesToDisplay/2d) - 1;
                        var below = (currentPage - middle);
                        var above = (currentPage + middle);

                        if (below < 4)
                        {
                            above = nrOfPagesToDisplay;
                            below = 1;
                        }
                        else if (above > (pageCount - 4))
                        {
                            above = pageCount;
                            below = (pageCount - nrOfPagesToDisplay);
                        }

                        start = below;
                        end = above;
                    }

                    if (start > 3)
                    {
                        routeValueDict[currentPageName] = "1";
                        _renderPager.AppendFormat(linkFormat, html.RouteLink("1", routeValueDict));

                        routeValueDict[currentPageName] = "2";
                        _renderPager.AppendFormat(linkFormat, html.RouteLink("2", routeValueDict));

                        _renderPager.AppendFormat(linkDisabledFormat, "...");
                    }

                    for (var i = start; i <= end; i++)
                    {
                        if (i == currentPage || (currentPage <= 0 && i == 0))
                        {
                            _renderPager.AppendFormat(linkCurrentFormat, i);
                        }
                        else
                        {
                            routeValueDict[currentPageName] = i == 1 ? string.Empty : i.ToString(); //第一頁不要有頁碼
                            _renderPager.AppendFormat(linkFormat, html.RouteLink(i.ToString(), routeValueDict));
                        }
                    }
                    if (end < (pageCount - 3))
                    {
                        _renderPager.AppendFormat(linkDisabledFormat, "...");

                        routeValueDict[currentPageName] = (pageCount - 1).ToString();
                        _renderPager.AppendFormat(linkFormat, html.RouteLink((pageCount - 1).ToString(), routeValueDict));

                        routeValueDict[currentPageName] = pageCount.ToString();
                        _renderPager.AppendFormat(linkFormat, html.RouteLink(pageCount.ToString(), routeValueDict));
                    }
                }

                #endregion

                if (currentPage < totalPages)
                {
                    //處理下一頁的連結
                    routeValueDict[currentPageName] = currentPage + 1;
                    _renderPager.AppendFormat(linkFormat, html.RouteLink(nextPage, routeValueDict));
                }
                else
                {
                    _renderPager.AppendFormat(linkDisabledFormat, nextPage);
                }

                if (currentPage != totalPages)
                {
                    routeValueDict[currentPageName] = totalPages;
                    _renderPager.AppendFormat(linkFormat, html.RouteLink(lastPage, routeValueDict));
                }
                else
                {
                    _renderPager.AppendFormat(linkDisabledFormat, lastPage);
                }
            }

            return new MvcHtmlString(_renderPager.ToString());
        }
        #endregion
    }
}