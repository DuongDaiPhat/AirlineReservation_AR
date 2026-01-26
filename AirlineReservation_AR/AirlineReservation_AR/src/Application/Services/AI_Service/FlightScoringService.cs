using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services.AI_Service
{
    public class FlightScoringService
    {
        public double Calculate(double[] flightVector, double[] userVector)
        {
            double score = 0;
            for (int i = 0; i < flightVector.Length; i++)
                score += flightVector[i] * userVector[i];
            return score;
        }
    }


}
