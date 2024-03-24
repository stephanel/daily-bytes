# Learning - Castle Dynamic Proxy

Dynamic Proxy creates a transparent proxy for the real object at runtime for us, and we can intercept the calls to it, and add logic to the objects.

what we want to achieve:

- be able to use non-frozen freezable object just like any other object
- be able to check if the object is freezable
- be able to check if the object is frozen
- be able to freeze freezable object
- NOT be able to change state of the object after it has been frozen
