import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44329/',
  redirectUri: baseUrl,
  clientId: 'OrderProject_App',
  responseType: 'code',
  scope: 'offline_access OrderProject',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'OrderProject',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44329',
      rootNamespace: 'Acme.OrderProject',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
