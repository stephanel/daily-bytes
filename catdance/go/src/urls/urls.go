package urls

type (
	urls struct {
		HOME_PATH string
	}
)

func ReturnURLS() urls {
	var urlPatterns urls
	urlPatterns.HOME_PATH = "/"
	return urlPatterns
}
