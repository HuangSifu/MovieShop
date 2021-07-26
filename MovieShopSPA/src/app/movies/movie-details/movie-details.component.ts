import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from 'src/app/core/services/movie.service';
import { Movie } from 'src/app/shared/models/Movie';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  movie!: Movie;
  id!: number;
  constructor(private movieservice: MovieService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.route.paramMap.subscribe(p => {
      this.id = +p.get('id')!;
      console.log(this.id);
      this.movieservice.getMovieDetails(this.id)
        .subscribe(m => {
          this.movie = m;
          console.log(this.movie);
        })
    });
  }

}