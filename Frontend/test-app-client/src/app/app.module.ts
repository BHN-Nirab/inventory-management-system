import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { NewsFeedComponent } from './news-feed/news-feed.component';
import { ChatListComponent } from './chat-list/chat-list.component';
import { StudentComponent } from './student/student.component';
import { StudentService } from './student.service';
import { AuthService } from './auth.service';
import { AuthComponent } from './auth/auth.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './/app-routing.module';
import { ProductService } from './product.service';


@NgModule({
  declarations: [
    AppComponent,
    NewsFeedComponent,
    ChatListComponent,
    StudentComponent,
    AuthComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  bootstrap: [AppComponent],
  providers: [StudentService, AuthService, ProductService]
})
export class AppModule { }
