using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_Solution
{
    

        static class NumtoTextBG
        {


            public static string Dig2Txt(string instring)
            {
                string mil = "милион";
                string mila = " милиона";
                string mild = "милиард";
                string milda = " милиарда";
                string hil = "хиляда";
                string hili = " хиляди";
                string lw = " лева";
                string st = " стотинки";
                string[] Init =
        {
            "ед",
            "дв",
            "три",
            "четири",
            "пет",
            "шест",
            "седем",
            "осем",
            "девет",
        };

                string[] Stotin = 
            {
                "един",
                "два",
                "три",
                "четири",
                "пет",
                "шест",
                "седем",
                "осем",
                "девет",
                "десет",
                "единадесет",
                "дванадесет",
                "тринадесет",
                "четиринадесет",
                "петнадесет",
                "шестандесет",
                "седемнадесет",
                "осемнадесет",
                "деветнадесет",
                "двадесет",
                "тридесет",
                "четиридесет",
                "петдесет",
                "шестнадесет",
                "седемсдесет",
                "осемдесет",
                "деветдесет",
                "сто", 
                "двеста", 
                "триста",
                "четиристотин" ,
                "петстотин", 
                "шестстотин", 
                "седемстотин", 
                "осемстотин", 
                "деветстотин", 

            };



                int c1;
                string edin = "един";
                string dva = "два";
                string dve = "две";
                string wstr, fstr = "", ostring, cstrc;
                int LastDelimiter;
                wstr = instring;
                LastDelimiter = wstr.IndexOf(".");

                if (LastDelimiter > 0)
                {


                    fstr = wstr.Substring(LastDelimiter + 1, wstr.Length - LastDelimiter - 1);
                    if (fstr.Length < 2)
                    {
                        fstr = fstr + "0";
                        wstr = wstr.Substring(0, LastDelimiter);
                    }
                    else
                    {
                        fstr = wstr.Substring(LastDelimiter + 1, 2);
                        wstr = wstr.Substring(0, LastDelimiter);
                    }


                }

                ostring = "";
                c1 = 0;
                while (wstr != "" && wstr != "0")
                {

                    if (wstr.Length < 3)
                    {
                        wstr = "000".Substring(0, 3 - wstr.Length) + wstr;

                    }


                    cstrc = Conv(wstr.Substring(wstr.Length - 3, 3), Init, false, c1);

                    switch (c1)
                    {
                        case 0:
                            if (cstrc != "")
                            {

                                if (wstr == "001")
                                {

                                    cstrc = edin;
                                    lw = " лев";

                                }
                                else
                                {
                                    if (wstr == "002")
                                    {

                                        cstrc = dva;

                                    }
                                }
                            }
                            else
                            {
                                cstrc = "";
                            }
                            break;
                        case 1:

                            if (int.Parse(wstr.Substring(wstr.Length - 3, 3)) > int.Parse("000"))
                            {

                                if (wstr == "001")
                                {
                                    cstrc = hil;

                                }
                                else
                                {
                                    if (wstr == "002")
                                    {

                                        cstrc = dve + "" + hili;

                                    }
                                    else
                                    {

                                        cstrc = cstrc + hili;

                                    }

                                }
                            }
                            break;
                        case 2:
                            if (int.Parse(wstr.Substring(wstr.Length - 3, 3)) > int.Parse("000"))
                            {
                                if (wstr == "001")
                                {
                                    cstrc = mil;

                                }
                                else
                                {

                                    cstrc = cstrc + mila;
                                }

                            }
                            break;
                        case 3:
                            if (int.Parse(wstr.Substring(wstr.Length - 3, 3)) > int.Parse("000"))
                            {

                                if (wstr == "001")
                                {
                                    cstrc = mild;

                                }
                                else
                                {

                                    cstrc = cstrc + milda;
                                }
                            }

                            break;

                    }

                    if (c1 > 0)
                    {
                        if (ostring != "")
                        {


                            if (wstr != "" && Array.Exists(Stotin, element => element == ostring))
                            {

                                ostring = cstrc + " и " + ostring;
                            }
                            else
                            {
                                ostring = cstrc + " " + ostring;
                            }

                        }
                        else
                        {

                            ostring = cstrc;
                        }


                    }
                    else
                    {
                        ostring = cstrc;
                    }

                    if (wstr.Length > 3)
                    {
                        wstr = wstr.Substring(0, wstr.Length - 3);
                    }
                    else
                    {
                        wstr = "";
                    }
                    c1 = c1 + 1;

                }


                if (fstr.Length > 0)
                {

                    cstrc = Conv(fstr, Init, true, c1);
                    ostring = ostring.TrimEnd();
                    if (ostring != "")
                    {
                        if (fstr == "01")
                        {

                            st = " стотинка";
                        }
                        return ostring + lw + " и " + cstrc + st;
                    }
                    else
                    {
                        if (fstr == "01")
                        {
                            st = " стотинка";

                        }
                        return cstrc + st;
                    }

                }
                else
                {
                    return ostring + lw;
                }



            }


            private static string Conv(string sstr, string[] Init, bool stotinka, int c)
            {
                string rstr, astrc = "", bstrc = "", cstrc = "";
                int apos, bpos, cpos;
                string jn = "ин";
                string na = "на";
                string sto = "сто";
                string dwesta = "двеста";
                string trista = "триста";
                string stotin = "стотин";
                string deset = "десет";
                string i = " и ";
                if (sstr.Length == 3)
                {

                    apos = int.Parse(sstr.Substring(2, 1));
                    bpos = int.Parse(sstr.Substring(1, 1));
                    cpos = int.Parse(sstr.Substring(0, 1));

                }
                else
                {

                    apos = int.Parse(sstr.Substring(1, 1));
                    bpos = int.Parse(sstr.Substring(0, 1));
                    cpos = 0;

                }
                if (apos == 1)
                {
                    if (sstr.Length == 3 || sstr.Length == 2)
                    {
                        if (stotinka)
                        {
                            astrc = Init[apos - 1] + na;
                        }
                        else
                        {
                            astrc = Init[apos - 1] + jn;
                        }
                        if (c > 0)
                        {

                            astrc = Init[apos - 1] + na;
                        }
                    }
                    else
                    {

                        astrc = Init[apos - 1] + na;
                    }
                }
                else
                {
                    if (apos == 2)
                    {

                        if (sstr.Length == 3 || sstr.Length == 2)
                        {
                            if (stotinka)
                            {
                                if (sstr == "12")
                                {

                                    astrc = Init[apos - 1] + "а";
                                }
                                else
                                {

                                    astrc = Init[apos - 1] + "е";

                                }

                            }
                            else
                            {
                                astrc = Init[apos - 1] + "а";
                            }

                            if (c == 1 && !stotinka)
                            {
                                if (sstr == "002")
                                {
                                    astrc = Init[apos - 1] + "е";
                                }
                                else
                                {

                                    astrc = Init[apos - 1] + "а";
                                }


                            }

                        }
                        else
                        {
                            if (c > 0)
                            {

                                astrc = Init[apos - 1] + "е";
                            }

                        }


                    }
                    else
                    {
                        if ((apos >= 3) && (apos <= 9))
                        {
                            astrc = Init[apos - 1];

                        }

                    }
                }


                if (bpos == 1)
                {
                    if (apos == 1)
                    {

                        // bstrc = astrc + "a" + deset;
                        bstrc = "единадесет";
                        astrc = "";
                    }
                    else
                    {
                        if (apos == 0)
                        {
                            bstrc = deset;
                            astrc = "";
                        }
                        else
                        {
                            if ((apos >= 2) && (apos <= 9))
                            {
                                bstrc = astrc + na + deset;
                                astrc = "";
                            }
                        }

                    }
                }

                if (bpos == 2)
                {
                    bstrc = Init[bpos - 1] + "а" + deset;
                }
                else
                {
                    if ((bpos >= 3) && (bpos <= 9))
                    {
                        bstrc = Init[bpos - 1] + deset;
                    }
                }
                switch (cpos)
                {
                    case 1:
                        cstrc = sto;
                        break;
                    case 2:
                        cstrc = dwesta;
                        break;
                    case 3:
                        cstrc = trista;
                        break;
                    default:
                        if (((cpos >= 4) && (cpos <= 9)))
                        {
                            cstrc = Init[cpos - 1] + stotin;
                        }
                        break;


                }
                rstr = astrc;
                if (cstrc.Length > 0)
                {
                    if (astrc.Length > 0)
                    {
                        if (bstrc.Length > 0)
                        {
                            rstr = cstrc + " " + bstrc + i + rstr;
                        }
                        else
                        {
                            rstr = cstrc + i + rstr;
                        }
                    }
                    else
                    {
                        if (bstrc.Length > 0)
                        {



                            rstr = cstrc + i + bstrc;

                        }
                        else
                        {

                            rstr = cstrc;


                        }

                    }
                }
                else
                {
                    if (bstrc.Length > 0)
                    {
                        if (c > 0)
                        {
                            if (astrc.Length > 0)
                            {
                                rstr = bstrc + i + rstr;
                            }
                            else
                            {

                                rstr = bstrc;
                            }
                        }
                        else
                        {
                            if (astrc.Length > 0)
                            {
                                rstr = bstrc + i + rstr;
                            }
                            else
                            {
                                rstr = bstrc;
                            }
                        }


                    }



                }
                return rstr;
            }

        }
    }

