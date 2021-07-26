import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/movieCard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  movies!: MovieCard[];

  constructor(private movieService: MovieService) { }

  //  is part of Component Life Cycle Hooks
  // certain events
  // Person => born => 1st event => 1st day at school, Marriage day.... Deadth day
  // COmpoent
  ngOnInit(): void {
    console.log('inide the ngOninit method of Home Component')
    // An ngOnInit() is a good place for a component to fetch its initial data.
    this.movieService.getTopRevenueMovies()
      .subscribe(m => {
        this.movies = m;
        console.log(this.movies);
      })
  }

}
