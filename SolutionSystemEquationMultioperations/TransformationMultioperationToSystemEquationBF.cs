﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionSystemEquationMultioperations
{
    class TransformationMultioperationToSystemEquationBF
    {
        public string[][] transformation(Dictionary<string, Multioperation> multioperations, string equation)
        {
            string[] equationSplit = equation.Split('<');
            redefinition(multioperations);
            string[][] equationLeftPart = multioperations[equationSplit[0]].equationPresent;
            string[][] equationRightPart = multioperations[equationSplit[1]].equationPresent;
            string[][] result = new string[equationLeftPart.Length][];

            for (int i = 0; i < equationLeftPart.Length; i++)
            {
                string[] s = new string[1];
                s[0] = equationLeftPart[i][0] + "<" + equationRightPart[i][0];
                result[i] = s;
            }

            return result;
        }

        private void redefinition(Dictionary<string, Multioperation> multioperations)
        {
            helpers.forTMToSEBF TMToSEBF = new helpers.forTMToSEBF();
            bool flag = true;
            while (flag)
            {
                int count = 0;
                foreach (KeyValuePair<string, Multioperation> kvp in multioperations)
                {
                    if (kvp.Value.equationPresent == null)
                    {
                        int countEquations = 0;
                        string[] arguments = kvp.Value.arguments;
                        foreach (string argument in arguments)
                        {
                            if (multioperations[argument].equationPresent == null) break;
                            countEquations++;
                        }
                        if (countEquations == arguments.Length)
                        {
                            TMToSEBF.getEquationPresent(multioperations, kvp.Key);
                            count++;
                        }
                    }
                    else
                    {
                        count++;
                    }
                }
                if (count == multioperations.Count) flag = false;
            }
        }
    }
}
