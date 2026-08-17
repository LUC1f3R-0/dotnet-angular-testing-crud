import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Service()
export class HomeService {
  private http = inject(HttpClient);
  
  getPokemon() {
      return this.http.get(`http://localhost:5262/api/users`);
  }
}
