import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Hero } from '../models/hero.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  GetHeroes(): Observable<Hero[]> {
    return this.http.get<Hero[]>(`${this.apiUrl}/Heroes/`);
  }

  AddHero(hero: Hero): Observable<Hero> {
    return this.http.post<Hero>(`${this.apiUrl}/Heroes/`, hero);
  }

  disableHero(id: number): Observable<Hero> {
    return this.http.delete<Hero>(`${this.apiUrl}/Heroes/?id=${id}`);
  }
}
