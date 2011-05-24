using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sessao5Practica
{
    public struct RationalNumber : IComparable<RationalNumber>, IEquatable<RationalNumber>
    {
        private readonly int _numerator, _denominator;

        public int Numerator { get { return this._numerator; } }
        public int Denominator { get { return this._denominator; } }

        public RationalNumber(int numerator, int denominator)
        {
            //if (denominator == 0) throw new InvalidRationalException();
            if (denominator == 0) throw new DivideByZeroException();
            _numerator = numerator;
            _denominator = denominator;
        }

        public static RationalNumber operator +(RationalNumber n1, RationalNumber n2)
        {
            int nn, nd;
            if (n1._denominator == n2._denominator)
            {
                nn = n1._numerator + n2._numerator;
                nd = n1._denominator;
            }
            else
            {
                nn = n1._numerator * n2._denominator + n2._numerator * n1._denominator;
                nd = n1._denominator * n2._denominator;
            }

            return new RationalNumber(nn, nd);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(_numerator);
            sb.Append('/');
            sb.Append(_denominator);
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is RationalNumber))
            {
                return false;
            }

            RationalNumber rn = (RationalNumber) obj;

            return this._numerator == rn._numerator && this._denominator == rn._denominator;
        }

        public override int GetHashCode()
        {
            return this.Numerator ^ this.Denominator;
        }

        public int CompareTo(RationalNumber other)
        {
            return (Convert.ToInt32(this.Numerator) / Convert.ToInt32(this.Denominator)) -
                   (Convert.ToInt32(other.Numerator) / Convert.ToInt32(other.Denominator));
        }

        public bool Equals(RationalNumber other)
        {
            return this.CompareTo(other) == 0;
        }
    }
}
