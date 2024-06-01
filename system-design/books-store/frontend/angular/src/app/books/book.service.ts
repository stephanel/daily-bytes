import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/local";
import { Book } from "src/app/books/book.model";

@Injectable({
    providedIn: 'root'
})
export class BookService {

    baseUrl = `${environment.baseUrl}/books-service/books`

    constructor(private httpClient: HttpClient) 
    { }

    get(id: number): Observable<Book> {
        return this.httpClient.get<Book>(`${this.baseUrl}/${id}`);
    }

    getAll(): Observable<Book[]> {
        return this.httpClient.get<Book[]>(this.baseUrl);
    }
}