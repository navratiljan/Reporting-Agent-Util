## Simple Reporting Agent 
### Intro popis 
Jednoduchý report agent, který zavolá Python script (ten je zodpovědný za generování statistik v .csv formátu). Agent má za úkol tato data agregovat do požadovaného formátu, konvertovat na formát .xlsx a zaslat tento report emailem.

O jakých datech je řeč ? <br/>
Python script generuje soubor v InputStatistics/Comibined_Watchlist_Statistics.csv.
Watchlist je seznam klientů, kteří sledují určitý počet companies. Máme dva typy tohoto sledování "Screening" a "Monitoring".<br/>
V .csv soubor jsou následující pole:
- Unifikátní identifikikátor klienta
- Typ
- Počet companies

### Funkce agenta
Funkce agenta je agregovat data následujícím způsobem:
1. Seskupit data dle unikátního identifikátoru klienta
2. Určit jestli se jedná o data co se týče "Screening" typu či "Monitoring" typu
3. Pokud se jedná o screening, udělat sumu hodnot počtu companies. Pokud se jedná o monitoring, najít max hodnotu v záznamech počtu companies.

Po agregaci jsou data uložena .xlsx formátu s dvěma sheety, jeden pro "Screning" a druhý pro "Monitoring". Excel soubor je následně zaslán na předdefinovanou emailovou adresu.

### Simple flowchart
![AgentFlowchart](https://user-images.githubusercontent.com/93519893/167251461-ee398ce1-a9be-4c5c-bd22-4e9d642f7734.png)


