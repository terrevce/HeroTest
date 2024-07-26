import { Component } from '@angular/core';
import { HeroService } from '../services/hero.service';
import { Hero } from '../models/hero.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(private heroSvc: HeroService) { }
  heroes: Hero[] = [];

  ngOnInit() {
    this.heroSvc.GetHeroes().subscribe({
      next: (data) => {
        this.heroes = data;
        console.log(this.heroes);
      },
      error: (error) => {
        console.error('get hero failed: ', error);
      }
    })
  }

  disableHero(id: number) 
  {
    this.heroSvc.disableHero(id).subscribe({
      error: (error) => {
        console.error(`delete hero failed: `,error);
      },
      complete: () =>{
        this.ngOnInit();    
      }
    })
    
  }

}
