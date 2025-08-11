import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/customers',
        name: '::Menu:Customers',
        iconClass: 'fas fa-users',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/stocks',
        name: '::Menu:Stocks',
        iconClass: 'fas fa-boxes',
        order: 3,
        layout: eLayoutType.application,
      },
      {
        path: '/orders',
        name: '::Menu:Orders',
        iconClass: 'fas fa-shopping-cart',
        order: 4,
        layout: eLayoutType.application,
      },
  ]);
}
