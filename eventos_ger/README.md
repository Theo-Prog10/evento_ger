# DOCUMENTAÇÃO DO PROJETO
## EventMaster

SÚMARIO

### EQUIPE

| Nome                     | Papel         |
|--------------------------|---------------|
| Eduardo Pitanga Loureiro | Desenvolvedor |
| Theo Mischiatti Gomes    | Desenvolvedor |

### OBJETIVO ESTRATÉGICO DO PROJETO

**O objetivo estratégico do projeto é definido pela seguinte frase:** </br>
"Nosso objetivo é fornecer uma plataforma intuitiva e eficiente que permita a criação, organização e gestão de eventos de forma simplificada, proporcionando uma experiência otimizada para organizadores e participantes, com foco em aumentar a produtividade, reduzir custos e melhorar a interação nos eventos."

### RESUMO DO PROJETO

O EventMaster é uma plataforma digital desenvolvida para facilitar a criação e a gestão de eventos, além de permitir o controle e a organização de participantes, palestrantes e organizadores. O sistema é estruturado de forma a atender diferentes tipos de usuários, sendo eles: participantes, palestrantes e organizadores, cada um com suas características e responsabilidades dentro da plataforma. Todos esses usuários possuem informações comuns, como nome, CPF e data de nascimento, mas também têm atributos específicos que os definem em suas funções.
</br>Os participantes possuem informações adicionais, como o tipo de ingresso adquirido, o status da inscrição e os eventos nos quais estão inscritos. Já os palestrantes têm uma biografia e uma especialidade descritas, além de estarem associados às palestras que irão ministrar durante os eventos. Por fim, os organizadores são responsáveis por gerenciar e coordenar os eventos, sendo identificados pelo seu contato (como telefone e e-mail) e pelos eventos que estão organizando.
</br>Um evento dentro da plataforma é composto por várias informações, como o nome, descrição, data, horário, local, o organizador responsável, os palestrantes envolvidos e os participantes que estão inscritos. O evento precisa estar associado a um organizador e a um local previamente cadastrados no sistema, sendo que a associação de palestrantes e participantes pode ser feita posteriormente, conforme a evolução do planejamento do evento.
</br>O local de um evento é composto por informações como nome, logradouro, número, UF (Unidade Federativa), cidade e bairro, permitindo que os eventos sejam mapeados para um espaço específico. Para a criação de um evento, é obrigatório associar um organizador e um local que já estejam cadastrados no sistema. Palestrantes e participantes podem ser adicionados ao evento mais tarde, conforme as necessidades do evento e as confirmações de presença.
</br>Além disso, o sistema oferece a capacidade de inserir e remover tanto participantes quanto palestrantes de um evento. Isso é necessário porque ambos podem, eventualmente, cancelar sua presença. O sistema precisa refletir essas mudanças, garantindo que a lista de participantes e palestrantes esteja sempre atualizada.
</br>A plataforma também permite apagar e atualizar qualquer uma das entidades cadastradas no sistema, sejam eventos, pessoas (participantes, palestrantes ou organizadores) ou locais. No caso de exclusão de um evento, é preciso desassociar todos os participantes, palestrantes e organizadores vinculados ao evento. A mesma lógica se aplica para a exclusão de uma pessoa ou um local. Quando uma pessoa é excluída, ela deve ser removida de todos os eventos nos quais está associada, com exceção do organizador, que não pode ser excluído se estiver vinculado a um evento ativo. Isso garante que os eventos sempre tenham um organizador designado. Da mesma forma, quando um local é excluído, todos os eventos que estavam associados a esse local devem ser desassociados do local, embora o evento em si continue existindo. Esse comportamento assegura a integridade dos dados e a consistência do sistema.
</br>Em resumo, o EventMaster foi desenvolvido para oferecer uma solução completa para a criação e gestão de eventos, com funcionalidades que permitem a inclusão, remoção e atualização de participantes, palestrantes e organizadores, bem como a criação e edição de eventos e locais. A plataforma foi projetada para garantir a flexibilidade de gerenciamento dos eventos, permitindo que o processo de organização seja eficiente, com a possibilidade de realizar alterações à medida que as necessidades dos participantes e palestrantes mudam. Além disso, as regras de exclusão e associação entre as entidades asseguram a integridade dos dados e a consistência nas operações do sistema.

