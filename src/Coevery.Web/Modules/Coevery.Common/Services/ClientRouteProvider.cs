﻿using Coevery.Mvc.ClientRoute;

namespace Coevery.Common.Services {
    public class ClientRouteProvider : ClientRouteProviderBase {
        public ClientRouteProvider() {
            IsFrontEnd = true;
        }

        public override void Discover(ClientRouteTableBuilder builder) {
            var navigationView = new ClientViewDescriptor() {
                Name = "menulist@",
                TemplateProvider = @"['$http', '$stateParams', function ($http, $stateParams) {
                        var url = '" + ModuleBasePath + @"ViewTemplate/MenuList/'+ $stateParams.NavigationId;
                        return $http.get(url).then(function (response) { return response.data; });
                    }]",
                Controller = "NavigationCtrl"
            };
            navigationView.AddDependencies(ToAbsoluteScriptUrl, "controllers/navigationcontroller");

            builder.Describe("Root")
                .Configure(descriptor =>
                {
                    descriptor.Abstract = true;
                    descriptor.Url = "";
                    descriptor.Views.Add(navigationView);
                });

            builder.Describe("Root.Navigation")
                .Configure(descriptor =>
                {
                    descriptor.Url = "/";
                });

            builder.Describe("Root.Menu")
              .Configure(descriptor =>
              {
                  descriptor.Url = "/{NavigationId:[0-9]+}";
              });

            builder.Describe("Root.Menu.List")
                .Configure(descriptor =>
                {
                    descriptor.Url = "/{Module:[a-zA-Z]+}";
                })
                .View(view =>
                {
                    view.Name = "@";
                    view.TemplateProvider = @"['$http', '$stateParams', function ($http, $stateParams) {
                        var url = '" + BasePath + @"' + $stateParams.Module + '/ViewTemplate/List/' + $stateParams.Module;
                        return $http.get(url).then(function (response) { return response.data; });
                    }]";
                    view.Controller = "GeneralListCtrl";
                    view.AddDependencies(ToAbsoluteScriptUrl, new[] { "controllers/listcontroller" });
                });

            builder.Describe("Root.Menu.Create")
                .Configure(descriptor =>
                {
                    descriptor.Url = "/{Module:[a-zA-Z]+}/Create";
                })
                .View(view =>
                {
                    view.Name = "@";
                    view.TemplateProvider = @"['$http', '$stateParams', function ($http, $stateParams) {
                        var url = '" + BasePath + @"' + $stateParams.Module + '/ViewTemplate/Create/' + $stateParams.Module;
                        return $http.get(url).then(function (response) { return response.data; });
                    }]";
                    view.Controller = "GeneralDetailCtrl";
                    view.AddDependencies(ToAbsoluteScriptUrl, new[] { "controllers/detailcontroller" });
                });

            builder.Describe("Root.Menu.Detail")
                .Configure(descriptor =>
                {
                    descriptor.Url = "/{Module:[a-zA-Z]+}/{Id:[0-9a-zA-Z]+}";
                })
                .View(view =>
                {
                    view.Name = "@";
                    view.TemplateProvider = @"['$http', '$stateParams', function ($http, $stateParams) {
                        var url = '" + BasePath + @"'+ $stateParams.Module + '/ViewTemplate/Edit/' + $stateParams.Id;
                        return $http.get(url).then(function (response) { return response.data; });
                    }]";
                    view.Controller = "GeneralDetailCtrl";
                    view.AddDependencies(ToAbsoluteScriptUrl, new[] { "controllers/detailcontroller" });
                });

            builder.Describe("Root.Menu.View")
                .Configure(descriptor =>
                {
                    descriptor.Url = "/{Module:[a-zA-Z]+}/View/{Id:[0-9a-zA-Z]+}";
                })
                .View(view =>
                {
                    view.Name = "@";
                    view.TemplateProvider = @"['$http', '$stateParams', function ($http, $stateParams) {
                        var url = '" + BasePath + @"' + $stateParams.Module + '/ViewTemplate/View/' + $stateParams.Id;
                        return $http.get(url).then(function (response) { return response.data; });
                    }]";
                    view.Controller = "GeneralViewCtrl";
                    view.AddDependencies(ToAbsoluteScriptUrl, "controllers/viewcontroller");
                });
        }
    }
}