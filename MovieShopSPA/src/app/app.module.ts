import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './core/layout/header/header.component';
import { GenresComponent } from './core/layout/genres/genres.component';
import { MovieCardComponent } from './shared/component/movie-card/movie-card.component';
import { CreateCastComponent } from './admin/create-cast/create-cast.component';
import { CreateMovieComponent } from './admin/create-movie/create-movie.component';
import { UpdateMovieComponent } from './admin/update-movie/update-movie.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    GenresComponent,
    MovieCardComponent,
    CreateCastComponent,
    CreateMovieComponent,
    UpdateMovieComponent,
    MovieDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
