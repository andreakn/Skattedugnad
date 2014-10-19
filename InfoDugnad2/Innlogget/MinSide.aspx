<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="..\Site.Master" CodeBehind="MinSide.aspx.cs" Inherits="InfoDugnad2.MinSide" %>

<%@ Import Namespace="InfoDugnad2.Utilities" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="jumbotron">
        <%if (!Me.IsBlocked)
          { %>

        <%if (AnsweredRequests.Any())
          { %>
        <h3>Besvarte spørsmål:</h3>
        <i>OBS! Etter at svaret er sett så vil det slettes.</i>
        <ul>
            <% foreach (var r in AnsweredRequests)
               { %>
            <li><a href="/Innlogget/Svar?id=<%:r.Id %>"><%:r.RequestInfo %></a> <i>(besvart <%:r.AnsweredDate.Friendly() %>)</i></li>
            <%  } %>
        </ul>
        <%} %>

        <%if (UnansweredRequests.Any())
          {%>
        <h3>Ubesvarte spørsmål:</h3>
        <ul>
            <% foreach (var r in UnansweredRequests)
               { %>
            <li><%:r.RequestInfo %> <i>(spurt <%:r.RequestedDate.Friendly() %>)</i></li>
            <%  } %>
        </ul>
        <%} %>

        <%if (TooHardRequests.Any())
          {%>
        <h3>Spørsmål ingen kunne besvare:</h3>
        <ul>
            <% foreach (var r in TooHardRequests)
               { %>
            <li><%:r.RequestInfo %> <i>(spurt <%:r.RequestedDate.Friendly() %>)</i></li>
            <%  } %>
        </ul>



        <%} %>

        <div>
            Du har <%:Me.Score %> poeng. Tjen poeng ved å svare på andres spørsmål. Bruk poeng på å stille egne spørsmål
        </div>
        <div>
            <a href="/Innlogget/FinnSporsmal">Svar på andres spørsmål</a>
        </div>

        <%if (Me.Score > 0)
          { %>
        <div>
            <a href="/Innlogget/StillSporsmal">Still spørsmål selv</a>
        </div>
        <%} %>


        <%}
          else
          { %>
            Beklager, for mange av spørsmålene/svarene dine er bedømt som upassende, du vil ikke lenger kunne svare eller lese svar.
        <%} %>
    </div>

</asp:Content>
