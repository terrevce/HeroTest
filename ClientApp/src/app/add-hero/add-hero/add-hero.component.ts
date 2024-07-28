import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Hero } from 'src/app/models/hero.model';
import { Router } from '@angular/router';
import { HeroService } from 'src/app/services/hero.service';
import { Brand } from 'src/app/models/brand.model';

@Component({
  selector: 'app-add-hero',
  templateUrl: './add-hero.component.html',
  styleUrls: ['./add-hero.component.css']
})
export class AddHeroComponent {

  heroForm = new FormGroup({
    name: new FormControl("", [Validators.required,Validators.maxLength(100)]),
    alias: new FormControl("", [Validators.maxLength(50), Validators.required]),
    brandName: new FormControl("", [Validators.required,Validators.maxLength(100)]),

  });
  hero: Hero = { Id: 0, Name: '', Alias: '', BrandName: '', };
  //TODO get brands from DB
  brands: Brand[] = [];

  constructor(private router: Router, private heroSvc: HeroService) { }

  
  ngOnInit() {
    this.heroSvc.GetBrands().subscribe({
      next: (data) => {
        this.brands = data;
        console.log(this.brands);
      },
      error: (error) => {
        console.error('get brands failed: ', error);
      }
    })
  }

  OnSubmitHero() {
    if (this.heroForm.valid) {
      this.hero = this.heroForm.value as Hero;
      //console.log(this.hero);
      this.heroSvc.AddHero(this.hero).subscribe({
        next: (hero) => {
          console.log('Hero added successfully:', hero);

        },
        error: (error) => {
          console.error('Error adding hero:', error);
        },
        complete: () => { this.router.navigate(['/']); }
      });

    }

  }


}
