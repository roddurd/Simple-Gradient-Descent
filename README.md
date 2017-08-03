# Simple-Gradient-Descent
A simple implementation of nonlinear regression for modeling polynomial functions up to the fourth degree, using gradient descent.


As explained in the program, data files should have one value pair (X,Y) per line.
X and Y values should be separated by a comma, space, or tab.

This is just a toy program to help solidify my understanding of gradient descent as I learn.
This is not meant to be a super accurate program.

<b>Choosing good parameters</b>

<i>Learning rate</i>

A good rule of thumb: For linear functions, an alpha lying within 0.001 < a < 0.01 should suffice.
With each increase in order of the polynomial, the range of alpha should decrease by an order of magnitude.

<i>Number of iterations</i>

Objectively, assuming the learning rate is sufficiently small, the greater the number of iterations the better.
For a rule of thumb: linear functions should converge closely enough within 10000 iterations.
With each increase in order of the polynomial, the number of iterations should increase by half an order of magnitude (the program caps at 1 million iterations).

If you have any questions, for whatever reason, email me at roddurd@gmail.com
