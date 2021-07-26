import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/MovieCard';
import { environment } from 'src/environments/environment';
import {map} from 'rxjs/operators';
import { Movie } from 'src/app/shared/models/Movie';
@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }
  // make an ajax HTTP call to API https://localhost:44347/api/Movies/toprevenue
  getTopRevenueMovies(): Observable<MovieCard[]> {
    return this.http.get(`${environment.apiUrl}` + 'movies/toprevenue')
      .pipe(map(resp => resp as MovieCard[]));
  }

  getMovieDetails(id:number): Observable<Movie>{
    return this.http.get(`${environment.apiUrl}`+'movies/'+ `${id}`).pipe(
      map(resp => resp as Movie )
    );
  }
  // Ypoutube, chaneels user can subscribe
  // Channel1 => upload a new video => User => Subscribe to that channel, you can recieve the notofication
  // Publish/Subscribe => 
  // Observer pattern 
  // JS async 
  // C# async/await => Task => Promise
  // Http Call, Ajax call, Promises...
  // AngularJS => Promises
  // Angular => Observables => Rxjs => LINQ Methods =>
  // HttpClinet => HttpModule => Observables
  // Instead, you can use a series of operators to transform values as needed
  // var list  = empllist.where( e => e.salry> 3000).Whare();
  // subscribe() 

  // lazy collections over time

  // filter is equivalent to Where
  // map is equivalent to Select
  // every is equivalent to All
  // some is equivalent to Any
  // reduce is "kinda" equivalent to Aggregate (and also can be used to Sum)
      
  
}