import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Service()
export class HomeService {
  private http = inject(HttpClient);
  
  getPokemon(name: string) {
      return this.http.get(`${environment.apiUrl}pokemon/${name}`);
  }
}
