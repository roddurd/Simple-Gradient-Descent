# Simple-Gradient-Descent
A simple implementation of nonlinear regression for modeling polynomial functions up to the fourth degree, using gradient descent.


As explained in the program, data files should have one value pair (X,Y) per line.
X and Y values should be separated by a comma, space, or tab.

This is just a toy program to help solidify my understanding of gradient descent as I learn.
This is not meant to be a super accurate program.

<h2><b>Choosing good parameters</b></h2>

<i><b>Learning rate</b></i>

A good rule of thumb: For linear functions, an alpha lying within 0.001 < a < 0.01 should suffice.
With each increase in order of the polynomial, the range of alpha should decrease by an order of magnitude.
(example: a fourth degree polynomial should use a learning rate within 0.000001 < a < 0.00001 <i>should</i> suffice)

<i><b>Number of iterations</b></i>

Objectively, assuming the learning rate is sufficiently small, the greater the number of iterations the better.
For a rule of thumb: linear functions should converge closely enough within 10000 iterations.
With each increase in order of the polynomial, the number of iterations should increase by half an order of magnitude (the program caps at 1 million iterations).

<font color="red"><i>Note: these rules might suck. Mess around with parameters for yourself and see what works best.</i></font>

<h2><b>Contacting me</b></h2>

If you have any questions, for whatever reason, email me at roddurd@gmail.com
