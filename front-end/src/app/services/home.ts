import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { CrudUserGet, CrudUserPost } from '../model/crud-user';
import { environment } from '../../environments/environment.development';
import { ApiResponse } from '../model/api-response';

@Service()
export class HomeService {
  private http = inject(HttpClient);

  getUsers() {
    return this.http.get<ApiResponse<CrudUserGet[]>>(`${environment.apiUrl}/users`);
  }

  getUser(uuId: string) {
    return this.http.get<ApiResponse<CrudUserGet>>(`${environment.apiUrl}/users/${uuId}`);
  }

  postUser(user: CrudUserPost) {
    return this.http.post<ApiResponse<CrudUserPost>>(`${environment.apiUrl}/users`, user);
  }

  deleteUser(uuid: string) {
    return this.http.delete<ApiResponse<CrudUserGet[]>>(`${environment.apiUrl}/users/${uuid}`);
  }
}