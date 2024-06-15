package middlewares

import (
	"net/http"
)

func LoggingMiddleware(res http.ResponseWriter, req *http.Request, next http.HandlerFunc) {
	// Logic to write request information, i.e. headers, user agent etc to a log file.
	next(res, req)
}
