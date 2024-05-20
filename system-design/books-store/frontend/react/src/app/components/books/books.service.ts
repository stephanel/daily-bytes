import { environment } from '../../../../environments/local';
import Axios, {
    AxiosInstance,
    // AxiosInterceptorOptions, AxiosRequestConfig,
    // AxiosResponse,
    // CreateAxiosDefaults,
    // InternalAxiosRequestConfig
} from "axios";
import { Observable, defer } from 'rxjs';
import { map } from 'rxjs/operators';
import { Book } from './book.model';

export default class BooksService {
    private httpClient: AxiosInstance;

    constructor() {
        this.httpClient = Axios.create({ baseURL: `${environment.baseUrl}` });
    }

    public get(id: number): Observable<Book> {
        // return this.httpClient.get<Book>(`books/${id}`);
        return defer(() => this.httpClient.get<Book>(`books/${id}`)).pipe(map(result => result.data));        
    }

    public getAll(): Observable<Book[]> {
        // return this.httpClient.get<Book[]>(`books`);
        return defer(() => this.httpClient.get<Book[]>(`books`)).pipe(map(result => result.data));        
    }
}