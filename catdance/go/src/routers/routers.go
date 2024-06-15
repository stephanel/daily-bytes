package routers

import (
	"catdance/controllers"
	"catdance/urls"

	"github.com/gorilla/pat"
)

func GetRouter() *pat.Router {
	urlPatterns := urls.ReturnURLS()

	router := pat.New()
	router.Get(urlPatterns.HOME_PATH, controllers.HomeController)

	return router
}
