﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_5_th
{
    abstract class Function
    {
        public Function()
        { }
        public abstract string ToString();
        public abstract double Culc(double x);

        public abstract Function Diff();

    }
    class constanta : Function
    {
        double c;
        public constanta(double c)
        {
            this.c = c;
        }
        public override string ToString()
        {
            return Convert.ToString(c);
        }
        public override double Culc(double x)
        {
            return c;
        }
        public override Function Diff()
        {
            Function diff = new constanta(0);
            return diff;
        }
    }
    class Arg : Function
    {
        public Arg()
        {
        }
        public override string ToString()
        {
            return "x";
        }
        public override double Culc(double x)
        {
            return x;
        }
        public override Function Diff()
        {
            Function diff = new constanta(1);
            return diff;
        }

    }

    class sin : Function
    {
        Function arg;
        public sin(Function arg)
        {
            this.arg = arg;
        }
        public override string ToString()
        {
            return "sin(" + arg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Math.Sin(arg.Culc(x));
        }
        public override Function Diff()
        {
            Function diff = new cos(arg);
            Function diff2 = arg.Diff();
            diff = new mul(diff, diff2);
            return diff;
        }
    }

    class cos : Function
    {
        Function arg;
        public cos(Function arg)
        {
            this.arg = arg;
        }
        public override string ToString()
        {
            return "cos(" + arg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Math.Cos(arg.Culc(x));
        }
        public override Function Diff()
        {
            Function diff = new sin(arg);
            Function subone = new constanta(-1);
            diff = new mul(subone,diff);
            Function diff2 = arg.Diff();
            diff = new mul(diff, diff2);
            return diff;
        }
    }
    class tan : Function
    {
        Function arg;
        public tan(Function arg)
        {
            this.arg = arg;
        }
        public override string ToString()
        {
            return "tan(" + arg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Math.Tan(arg.Culc(x));
        }
        public override Function Diff()
        {
            Function pow2 = new constanta(2);
            Function cosx = new cos(arg);
            Function powcos = new pow(cosx,pow2);
            Function one = new constanta(1);
            Function diff = new div(one,powcos);
            Function diff2 = arg.Diff();
            diff = new mul(diff, diff2);
            return diff;
        }
    }
    class ctan : Function
    {
        Function arg;
        public ctan(Function arg)
        {
            this.arg = arg;
        }
        public override string ToString()
        {
            return "ctan(" + arg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return 1/Math.Tan(arg.Culc(x));
        }
        public override Function Diff()
        {
            Function pow2 = new constanta(2);
            Function sinx = new sin(arg);
            Function powcos = new pow(sinx, pow2);
            Function one = new constanta(-1);
            Function diff = new div(one, powcos);
            Function diff2 = arg.Diff();
            diff = new mul(diff, diff2);
            return diff;
        }
    }
    class exp : Function
    {
        Function arg;
        public exp(Function arg)
        {
            this.arg = arg;
        }
        public override string ToString()
        {
            return "exp(" + arg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Math.Exp(arg.Culc(x));
        }
        public override Function Diff()
        {
            Function diff = new exp(arg);
            Function diff2 = arg.Diff();
            diff = new mul(diff, diff2);
            return diff;
        }
    }
    class ln : Function
    {
        Function arg;
        public ln(Function arg)
        {
            this.arg = arg;
        }
        public override string ToString()
        {
            return "ln(" + arg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Math.Log(arg.Culc(x));
        }
        public override Function Diff()
        {
            Function one = new constanta(1);
            Function diff = new div(one,arg);
            Function diff2 = arg.Diff();
            diff = new mul(diff, diff2);
            return diff;
        }
    }
    class pow : Function
    {
        Function Larg, Rarg;
        public pow(Function Larg, Function Rarg)
        {
            this.Larg = Larg;
            this.Rarg = Rarg;
        }
        public override string ToString()
        {
            return "(" + Larg.ToString() + "^" + Rarg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Math.Pow(Larg.Culc(x),Rarg.Culc(x));
        }
        public override Function Diff()
        {
            Function pow = new pow(Larg, Rarg);
            Function ln = new ln(Larg);
            Function diff = new mul(pow,ln);
            Function diff2 = Rarg.Diff();
            diff = new mul(diff, diff2);
            return diff;
        }
    }

    class add : Function
    {
        Function Larg, Rarg;
        public add(Function Larg, Function Rarg)
        {
            this.Larg = Larg;
            this.Rarg = Rarg;
        }
        public override string ToString()
        {
            return "(" + Larg.ToString() + "+" + Rarg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Larg.Culc(x) + Rarg.Culc(x);
        }
        public override Function Diff()
        {
            Function diffLarg = Larg.Diff();
            Function diffRarg = Rarg.Diff();
            Function diff = new add(diffLarg, diffRarg);
            Function diff2 = Larg.Diff();
            diff = new mul(diff, Larg.Diff());
            diff2 = new mul(diff2, Rarg.Diff());
            diff = new add(diff, diff2);
            return diff;
        }
    }
    class sub : Function
    {
        Function Larg, Rarg;
        public sub(Function Larg, Function Rarg)
        {
            this.Larg = Larg;
            this.Rarg = Rarg;
        }
        public override string ToString()
        {
            return "(" + Larg.ToString() + "-" + Rarg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Larg.Culc(x) - Rarg.Culc(x);
        }
        public override Function Diff()
        {
            Function diffLarg = Larg.Diff();
            Function diffRarg = Rarg.Diff();
            Function diff = new sub(diffLarg, diffRarg);
            Function diff2 = Larg.Diff();
            diff = new mul(diff, Larg.Diff());
            diff2 = new mul(diff2, Rarg.Diff());
            diff = new sub(diff, diff2);
            return diff;
        }

    }
    class mul : Function
    {
        Function Larg, Rarg;
        public mul(Function Larg, Function Rarg)
        {
            this.Larg = Larg;
            this.Rarg = Rarg;
        }
        public override string ToString()
        {
            return "(" + Larg.ToString() + "*" + Rarg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Larg.Culc(x) * Rarg.Culc(x);
        }
        public override Function Diff()
        {
            Function diffLarg = Larg.Diff();
            Function diffRarg = Rarg.Diff();
            Function mul1 = new mul(diffLarg, Larg);
            Function mul2 = new mul(Rarg, diffRarg);
            mul1 = new mul(mul1, Larg.Diff());
            mul2 = new mul(mul2, Rarg.Diff());
            Function diff = new add(mul1, mul2);
            return diff;
        }
    }

    class div : Function
    {
        Function Larg, Rarg;
        public div(Function Larg, Function Rarg)
        {
            this.Larg = Larg;
            this.Rarg = Rarg;
        }
        public override string ToString()
        {
            return "(" + Larg.ToString() + "/" + Rarg.ToString() + ")";
        }
        public override double Culc(double x)
        {
            return Larg.Culc(x) / Rarg.Culc(x);
        }
        public override Function Diff()
        {
            Function diffLarg = Larg.Diff();
            Function diffRarg = Rarg.Diff();
            Function mul1 = new mul(diffLarg, Larg);
            Function mul2 = new mul(Rarg, diffRarg);
            mul1 = new mul(mul1, Larg.Diff());
            mul2 = new mul(mul2, Rarg.Diff());
            Function sub = new sub(mul1, mul2);
            Function square = new constanta(2);
            Function powRarg = new pow(Rarg, square);
            Function diff = new div(sub,powRarg);
            return diff;
        }
    }
}