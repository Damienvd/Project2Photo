import React, { lazy } from 'react';
import { renderRoutes } from 'react-router-config';
import { Redirect } from 'react-router-dom';

import AuthLayout from './layouts/Auth';
import HomeLayout from './layouts/Home';

const Routes = () => {

  const routes = [
    {
      path: '/',
      exact: true,
      component: () => <Redirect to='/home' />
    },
    {
      path: '/auth',
      component: AuthLayout,
      routes: [
        {
          path: '/auth/login',
          exact: true,
          component: lazy(() => import('./views/Auth/Login/Login'))
        },
      ]
    },
    {
      route: '*',
      component: HomeLayout,
      routes: [
        {
          path: '/home',
          exact: true,
          component: lazy(() => import('./views/Home/Home'))
        }
      ]
    }
  ];

  return (
    renderRoutes(routes)
  );
}

export default Routes;