import { Routes } from '@angular/router';
import { Home } from './home/home';
import { ConfirmTest } from './confirm-test/confirm-test';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'confirm', component: ConfirmTest }
];
