using System.Collections.Generic;
using System.Text;

namespace MyToDoList
{
    public class Where
    {
        private string _Campo;
        private string _Into;
        private string _Valor;
        private string _Completo;

        public Where(string pWhereCompleto)
        {
            _Completo = pWhereCompleto;
        }

        public Where(string pCampo, string pValor)
        {
            _Campo = pCampo;
            _Valor = pValor;
            _Completo = pCampo + " =  '" + pValor + "'";
        }

        public Where(string pCampo, string pSinal, int pValor)
        {
            _Campo = pCampo;
            _Valor = pValor.ToString();
            _Completo = pCampo + pSinal + pValor;
        }

        public Where(string pCampo, DateTime pData1, DateTime pData2)
        {
            _Campo = pCampo;
            _Valor = "'" + pData1.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + pData2.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            _Completo = pCampo + " BETWEEN " + _Valor;
        }

        public override string ToString()
        {
            return _Completo;
        }
    }

    public class Having
    {
        private string _Valor;

        public Having(string pWhereCompleto)
        {
            _Valor = pWhereCompleto;
        }

        public override string ToString()
        {
            return _Valor;
        }
    }

    public class Join
    {
        private string _TipoJoin;
        private string _Tabela;
        private string _Definicao;
        private bool _WithNoLock;

        public void Redefinir(string pTipoJoin, string pTabela, string pDefinicao, bool pWithNoLock)
        {
            _TipoJoin = pTipoJoin; _Tabela = pTabela; _Definicao = pDefinicao; _WithNoLock = pWithNoLock;
        }

        public Join(string pTipoJoin, string pTabela, string pDefinicao, bool pWithNoLock, bool pWithDbo)
        {
            _TipoJoin = pTipoJoin; _Tabela = pTabela; _Definicao = pDefinicao; _WithNoLock = pWithNoLock;
        }

        public override string ToString()
        {
            if (_WithNoLock)
                return string.Format(" {0} JOIN {1} (NOLOCK) ON {2}  ", _TipoJoin, _Tabela, _Definicao);
            else
                return string.Format(" {0} JOIN {1} ON {2} ", _TipoJoin, _Tabela, _Definicao);
        }
    }

    public class CrossApply
    {
        private string _Funcao;

        public CrossApply(string pFuncao)
        {
            _Funcao = pFuncao;
        }

        public override string ToString()
        {
            return string.Format(" CROSS APPLY {0} ", _Funcao);
        }
    }

    public class CrossJoin
    {
        private string _Tabela;
        private bool _WithNoLock;

        public CrossJoin(string pTabela, bool pWithNoLock)
        {
            _Tabela = pTabela; _WithNoLock = pWithNoLock;
        }

        public override string ToString()
        {
            if (_WithNoLock)
                return string.Format(" CROSS JOIN {0} (NOLOCK) ", _Tabela);
            else
                return string.Format(" CROSS JOIN {0} ", _Tabela);
        }
    }

    public class OuterApply
    {
        private string _Funcao;

        public OuterApply(string pFuncao)
        {
            _Funcao = pFuncao;
        }

        public override string ToString()
        {
            return string.Format(" OUTER APPLY {0} ", _Funcao);
        }
    }

    public class Select
    {
        private bool _NoLock = false;
        private bool _NoFrom = false;
        private bool _WithDbo = true;

        private string _Campos = string.Empty;
        private string _Into = string.Empty;
        private string _Tabela = string.Empty;
        private string _Alias = string.Empty;

        private List<Where> _Where = new List<Where>();
        private List<Join> _Joins = new List<Join>();
        private List<CrossJoin> _CrossJoins = new List<CrossJoin>();
        private List<Having> _Havings = new List<Having>();
        private List<CrossApply> _CrossApplys = new List<CrossApply>();
        private List<OuterApply> _OuterApplys = new List<OuterApply>();
        private string _SQL = string.Empty;
        private string _OrderBy = string.Empty;
        private int _Limit = 0;
        private string _DistinctRow = string.Empty;
        private string _GroupBy = string.Empty;

        public Select DistinctRow()
        {
            _DistinctRow = " DISTINCT ";

            return this;
        }

        public Select Limit(int pLimit)
        {
            _Limit = pLimit;

            return this;
        }

        public Select OrderBy(string pOrderBy)
        {
            if (_OrderBy.Equals(string.Empty))
                _OrderBy = pOrderBy;
            else
                _OrderBy += ", " + pOrderBy;

            return this;
        }

        public Select OrderBySubst(string pOrderBy)
        {
            _OrderBy = pOrderBy;
            return this;
        }

        public Select From(string pTabela)
        {
            _Tabela = pTabela;

            return this;
        }

        public Select GroupBy(string pGroupBy)
        {
            if (_GroupBy.Equals(string.Empty))
                _GroupBy = pGroupBy;
            else
                _GroupBy += ", " + pGroupBy;

            return this;
        }

        public Select From(string pTabela, string pAlias)
        {
            /*
            if (_WithDbo)
            {
                if (!pTabela.ToLower().Contains("dbo."))
                    pTabela = "dbo." + pTabela;
            }
            */

            _Tabela = pTabela;
            _Alias = pAlias;

            return this;
        }

        public Select()
        {
        }

        public Select(string pCampos)
        {
            _Campos = pCampos;
        }

        public Select(string pCampos, string pTabela)
        {
            _Tabela = pTabela;
            _Campos = pCampos;
        }

        public Select(string pCampos, string pTabela, string pWhere)
        {
            _Campos = pCampos;
            _Tabela = pTabela;
            _Where.Add(new Where(pWhere));
        }

        public Select(string pCampos, string pTabela, string pWhere, string pOrderBy)
        {
            _Campos = pCampos;
            _Tabela = pTabela;
            _Where.Add(new Where(pWhere));
            _OrderBy = pOrderBy;
        }

        public Select WithoutDbo()
        {
            _WithDbo = false;
            return this;
        }

        public Select WithNoLock()
        {
            _NoLock = true;
            return this;
        }

        public Select WithNoFrom()
        {
            _NoFrom = true;
            return this;
        }

        public Select LeftJoin(string pTabela, string pDefinicao)
        {
            _Joins.Add(new Join("LEFT", pTabela, pDefinicao, _NoLock, _WithDbo));

            return this;
        }

        public Select InnerJoinSubst(string pTabela, string pDefinicao, string pNovaTabela, string pNovaDefinicao)
        {
            var DadosJoin = new Join("INNER", pTabela, pDefinicao, _NoLock, _WithDbo);

            int Cont = 0;
            int Indice = -1;
            foreach (var J in _Joins)
            {
                string Aux = J.ToString();
                if (Aux == DadosJoin.ToString())
                {
                    Indice = Cont;
                    break;
                }

                Cont++;
            }

            if (Indice > -1)
            {
                var J = _Joins[Indice];
                J.Redefinir("INNER", pNovaTabela, pNovaDefinicao, _NoLock);
            }

            return this;
        }

        public Select InnerJoin(string pTabela, string pDefinicao)
        {
            var DadosJoin = new Join("INNER", pTabela, pDefinicao, _NoLock, _WithDbo);

            foreach (var J in _Joins)
                if (J.ToString() == DadosJoin.ToString())
                    return this;

            _Joins.Add(new Join("INNER", pTabela, pDefinicao, _NoLock, _WithDbo));

            return this;
        }

        public Select RightJoin(string pTabela, string pDefinicao)
        {
            _Joins.Add(new Join("RIGHT", pTabela, pDefinicao, _NoLock, _WithDbo));

            return this;
        }

        public Select FullJoin(string pTabela, string pDefinicao)
        {
            _Joins.Add(new Join("FULL", pTabela, pDefinicao, _NoLock, _WithDbo));

            return this;
        }

        public Select CrossJoin(string pTabela)
        {
            _CrossJoins.Add(new CrossJoin(pTabela, _NoLock));

            return this;
        }

        public Select WhereBooleanTrue(bool pVerificacao, string pCampo)
        {
            if (pVerificacao)
                return Where(pCampo + " = 1");
            else
                return this;
        }

        public Select WhereValorMaiorOuIgualAZero(int pValor, string pCampo)
        {
            if (pValor >= 0)
            {
                if (pCampo.IndexOf(".") == -1)
                    return Where(pCampo + " = @" + pCampo);
                else
                {
                    string CampoSemPonto = pCampo.Substring(pCampo.IndexOf(".") + 1);
                    return Where(pCampo + " = @" + CampoSemPonto);
                }
            }
            else
                return this;
        }

        public Select WhereValorMaiorZero(int pValor, string pCampo, string pParametro)
        {
            if (pValor > 0)
                return Where(pCampo + " = @" + pParametro);
            else
                return this;
        }

        public Select WhereValorMaiorZero(decimal pValor, string pCampo, string pParametro)
        {
            if (pValor > 0)
                return Where(pCampo + " = @" + pParametro);
            else
                return this;
        }

        public Select WhereValorMaiorZero(decimal pValor, string pCampo)
        {
            if (pValor > 0)
            {
                if (pCampo.IndexOf(".") == -1)
                    return Where(pCampo + " = @" + pCampo);
                else
                {
                    string CampoSemPonto = pCampo.Substring(pCampo.IndexOf(".") + 1);
                    return Where(pCampo + " = @" + CampoSemPonto);
                }
            }
            else
                return this;
        }

        public Select WhereLike(string pValor, string pCampo, string pParametro)
        {
            if (!string.IsNullOrEmpty(pValor))

                return Where(pCampo + " LIKE @" + pParametro);
            else
                return this;
        }

        public Select WhereNotLike(string pValor, string pCampo, string pParametro)
        {
            if (!string.IsNullOrEmpty(pValor))
            {
                if (pCampo.IndexOf(".") == -1)
                    return Where(pCampo + " NOT LIKE @" + pParametro);
                else
                {
                    string CampoSemPonto = pCampo.Substring(pCampo.IndexOf(".") + 1);
                    return Where(pCampo + " NOT LIKE @" + pParametro);
                }
            }
            else
                return this;
        }

        public Select WhereNotLike(string pValor, string pCampo)
        {
            if (!string.IsNullOrEmpty(pValor))
            {
                if (pCampo.IndexOf(".") == -1)
                    return Where(pCampo + " NOT LIKE @" + pCampo);
                else
                {
                    string CampoSemPonto = pCampo.Substring(pCampo.IndexOf(".") + 1);
                    return Where(pCampo + " NOT LIKE @" + CampoSemPonto);
                }
            }
            else
                return this;
        }

        public Select WhereLike(string pValor, string pCampo)
        {
            if (!string.IsNullOrEmpty(pValor))
            {
                if (pCampo.IndexOf(".") == -1)
                    return Where(pCampo + " LIKE @" + pCampo);
                else
                {
                    string CampoSemPonto = pCampo.Substring(pCampo.IndexOf(".") + 1);
                    return Where(pCampo + " LIKE @" + CampoSemPonto);
                }
            }
            else
                return this;
        }

        public Select WhereFaixaValores(decimal pValorInicial, decimal pValorFinal, string pCampo, string pParametroDe, string pParametroAte)
        {
            if ((pValorFinal > 0) && (pValorFinal >= pValorInicial))
                return Where(pCampo + " BETWEEN @" + pParametroDe + " AND @" + pParametroAte);
            else
                return this;
        }

        public Select WhereValorMaiorZero(int pValor, string pCampo)
        {
            if (pValor > 0)
            {
                if (pCampo.IndexOf(".") == -1)
                    return Where(pCampo + " = @" + pCampo);
                else
                {
                    string CampoSemPonto = pCampo.Substring(pCampo.IndexOf(".") + 1);
                    return Where(pCampo + " = @" + CampoSemPonto);
                }
            }
            else
                return this;
        }

        public Select Having(string pValor)
        {
            if (string.IsNullOrEmpty(pValor))
                return this;

            _Havings.Add(new Having(pValor));

            return this;
        }

        /// <summary>
        /// Monta o WHERE com uma lista de itens passados.  Não faz SELECT IN pois é mais lento. Faz com OR.
        /// </summary>
        /// <param name="pCampo"></param>
        /// <param name="pLista"></param>
        /// <returns></returns>
        public Select Where(string pCampo, List<int> pLista)
        {
            if (pLista.Count == 0)
                return this;

            var ListaDistinct = pLista.Distinct().ToList();

            string Str = "(";

            foreach (var Item in ListaDistinct)
            {
                //if (pLista.IndexOf(Item) == pLista.Distinct().Count())
                if (ListaDistinct.IndexOf(Item) == ListaDistinct.Count() - 1)
                    Str += pCampo + " = " + Item.ToString() + " ";
                else
                    Str += pCampo + " = " + Item.ToString() + " OR ";
            }

            Str += ")";

            _Where.Add(new Where(Str));

            return this;
        }

        public Select Where(string pCampo, List<string> pLista)
        {
            if (pLista.Count == 0)
                return this;

            var ListaDistinct = pLista.Distinct().ToList();

            string Str = "(";

            foreach (var Item in ListaDistinct)
            {
                //if (pLista.IndexOf(Item) == pLista.Distinct().Count())
                if (ListaDistinct.IndexOf(Item) == ListaDistinct.Count() - 1)
                    Str += pCampo + " LIKE '%" + Item.ToString() + "%' ";
                else
                    Str += pCampo + " LIKE '%" + Item.ToString() + "%' OR ";
            }

            Str += ")";

            _Where.Add(new Where(Str));

            return this;
        }

        public Select WhereListaComOROutroFiltro(string pCampo, List<int> pLista, string pOutroCampo)
        {
            if (pLista.Count == 0)
                return this;

            string Str = "((";

            var Lista = pLista.Distinct().ToList();

            foreach (var Item in Lista)
            {
                if (Lista.IndexOf(Item) == Lista.Count() - 1)
                    Str += pCampo + " = " + Item.ToString();
                else
                    Str += pCampo + " = " + Item.ToString() + " OR ";
            }

            Str += ") OR " + pOutroCampo + ")";

            _Where.Add(new Where(Str));

            return this;
        }

        public Select Where(string pValor)
        {
            if (string.IsNullOrEmpty(pValor))
                return this;

            _Where.Add(new Where(pValor));

            return this;
        }

        public Select Where(string pCampo, bool pValor)
        {
            if (pValor)
                _Where.Add(new Where(pCampo));

            return this;
        }

        public Select Where(string pCampo, int pValor)
        {
            _Where.Add(new Where(pCampo, pValor.ToString()));

            return this;
        }

        public Select Where(string pCampo, string pSinal, int pValor)
        {
            _Where.Add(new Where(pCampo, pSinal, pValor));

            return this;
        }

        public Select Where(string pCampo, DateTime pData1, DateTime pData2)
        {
            _Where.Add(new Where(pCampo, pData1, pData2));

            return this;
        }

        public Select ReplaceCampos(string pValor, string pValorSubst)
        {
            _Campos = _Campos.Replace(pValor, pValorSubst);
            return this;
        }

        public Select Campos()
        {
            _Campos = "*";

            return this;
        }

        public Select CamposSubst(string pCampos)
        {
            _Campos = pCampos;
            return this;
        }

        public Select Campos(string pCampos)
        {
            if (string.IsNullOrWhiteSpace(_Campos))
                _Campos = pCampos;
            else
                _Campos += ", " + pCampos;

            return this;
        }

        public Select OuterApply(string pFuncao)
        {
            _OuterApplys.Add(new OuterApply(pFuncao));

            return this;
        }

        public Select CrossApply(string pFuncao)
        {
            _CrossApplys.Add(new CrossApply(pFuncao));

            return this;
        }

        public Select Into(string pCampos)
        {
            _Into = pCampos;
            //_Tabela = pTabela;

            return this;
        }

        public Select Campos(string pCampos, string pTabela)
        {
            _Campos = pCampos;
            _Tabela = pTabela;

            return this;
        }

        private string GerarSQL()
        {
            string SQL = "SELECT " + _DistinctRow + " ";

            SQL += " " + _Campos + " ";


            if (_Into.Length > 0)
            {
                SQL += " INTO " + _Into;
            }


            if (!_NoFrom)
            {
                SQL += " FROM " + _Tabela;
                if (_NoLock)
                    SQL += " (NOLOCK) ";

                if (!string.IsNullOrEmpty(_Alias))
                    SQL += " AS " + _Alias;
            }

            SQL += "  ";

            foreach (CrossJoin CJ in _CrossJoins)
                SQL += CJ.ToString();

            foreach (Join J in _Joins)
                SQL += J.ToString();

            foreach (CrossApply CA in _CrossApplys)
                SQL += CA.ToString();

            foreach (OuterApply OA in _OuterApplys)
                SQL += OA.ToString();

            if (_Where.Count > 0)
            {
                SQL += " WHERE ";

                int ContWhere = 0;

                foreach (Where W in _Where)
                {
                    if (ContWhere == _Where.Count - 1) // ÚLTIMO
                        SQL += W.ToString();
                    else
                        SQL += W.ToString() + " AND ";

                    ContWhere++;
                }
            }

            if (_GroupBy.Length > 0)
                SQL += " GROUP BY " + _GroupBy;

            if (_Havings.Count > 0)
            {
                SQL += " HAVING ";

                int ContHaving = 0;

                foreach (Having W in _Havings)
                {
                    if (ContHaving == _Havings.Count - 1) // ÚLTIMO
                        SQL += W.ToString();
                    else
                        SQL += W.ToString() + " AND ";

                    ContHaving++;
                }
            }

            if (_OrderBy.Length > 0)
                SQL += " ORDER BY " + _OrderBy;

            if (_Limit > 0)
                SQL += " LIMIT " + _Limit.ToString();

            return SQL;
        }

        public override string ToString()
        {
            return GerarSQL();
        }

        public string ToStringUnionAll(Select pSQL2)
        {
            string SQL1 = GerarSQL();
            string SQL2 = pSQL2.GerarSQL();

            return SQL1 + " UNION ALL " + SQL2;
        }

        public string ToStringUnionAll(Select pSQL2, Select pSQL3)
        {
            string SQL1 = GerarSQL();
            string SQL2 = pSQL2.GerarSQL();
            string SQL3 = pSQL3.GerarSQL();

            return SQL1 + " UNION ALL " + SQL2 + " UNION ALL " + SQL3;
        }

        public string ToStringUnionAll(Select pSQL2, Select pSQL3, Select pSQL4)
        {
            string SQL1 = GerarSQL();
            string SQL2 = pSQL2.GerarSQL();
            string SQL3 = pSQL3.GerarSQL();
            string SQL4 = pSQL4.GerarSQL();

            return SQL1 + " UNION ALL " + SQL2 + " UNION ALL " + SQL3 + " UNION ALL " + SQL4;
        }

        public string ToStringUnionAll(Select pSQL2, Select pSQL3, Select pSQL4, Select pSQL5)
        {
            string SQL1 = GerarSQL();
            string SQL2 = pSQL2.GerarSQL();
            string SQL3 = pSQL3.GerarSQL();
            string SQL4 = pSQL4.GerarSQL();
            string SQL5 = pSQL5.GerarSQL();

            return SQL1 + " UNION ALL " + SQL2 + " UNION ALL " + SQL3 + " UNION ALL " + SQL4 + " UNION ALL " + SQL5;
        }

        public string ToStringUnionAll(Select pSQL2, Select pSQL3, Select pSQL4, Select pSQL5, Select pSQL6)
        {
            string SQL1 = GerarSQL();
            string SQL2 = pSQL2.GerarSQL();
            string SQL3 = pSQL3.GerarSQL();
            string SQL4 = pSQL4.GerarSQL();
            string SQL5 = pSQL5.GerarSQL();
            string SQL6 = pSQL6.GerarSQL();

            return SQL1 + " UNION ALL " + SQL2 + " UNION ALL " + SQL3 + " UNION ALL " + SQL4 + " UNION ALL " + SQL5 + " UNION ALL " + SQL6;
        }
    }

}
