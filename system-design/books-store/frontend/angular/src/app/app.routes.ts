import { Route } from '@angular/router';
import { BooksListComponent } from './books/books-list/books-list.component';
import { BookDetailsComponent } from './books/book-detail/book-details.component';
import { UserAccountComponent } from './users/user-account/user-account.component';
import { UserOrdersHistoryComponent } from './user-orders-history/user-orders-history/user-orders-history.component';
import { LoginComponent } from './security/login/login.component';
import { AuthGuard } from './security/auth.guard';

export const appRoutes: Route[] = [
    { path: '', redirectTo: "books", pathMatch: "full" },
    { path: 'books', component: BooksListComponent },
    { path: 'book/:id', component: BookDetailsComponent },
    { path: 'user/:id', component: UserAccountComponent, canActivate: [AuthGuard] },
    { path: 'user/:id/order-history', component: UserOrdersHistoryComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
];
