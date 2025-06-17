-- Limpar dados existentes (se necessário)
TRUNCATE TABLE chapas CASCADE;
TRUNCATE TABLE processos_serragem CASCADE;
TRUNCATE TABLE blocos CASCADE;
TRUNCATE TABLE usuarios CASCADE;

-- Inserir usuário administrador
INSERT INTO usuarios (nome, email, senha, cargo, data_cadastro, ativo)
VALUES (
    'Administrador',
    'admin@admin.com',
    'JxhFjtIM0GSNAgKL3/6akQ==.Qmqstf6nrjegw933/gG9+ORFSM9Ok2wGBB5l5nfMXlI=', -- senha: admin123#
    'Administrador',
    CURRENT_TIMESTAMP,
    true
);

-- Inserir blocos
INSERT INTO blocos (codigo, pedreira_origem, tipo_material, largura, altura, comprimento, valor_compra, nota_fiscal_entrada, data_cadastro, disponivel, "Cerrado")
VALUES 
    ('BL001', 'Pedreira São Thomé', 'Mármore Branco', 180, 160, 300, 15000, 'NF-001', CURRENT_TIMESTAMP, true, false),
    ('BL002', 'Pedreira Cachoeiro', 'Granito Preto São Gabriel', 190, 170, 310, 18000, 'NF-002', CURRENT_TIMESTAMP, true, false),
    ('BL003', 'Pedreira Rio Novo', 'Granito Amarelo Ornamental', 175, 155, 290, 16500, 'NF-003', CURRENT_TIMESTAMP, true, false),
    ('BL004', 'Pedreira Monte Verde', 'Mármore Travertino', 185, 165, 305, 17000, 'NF-004', CURRENT_TIMESTAMP, true, false),
    ('BL005', 'Pedreira Serra Alta', 'Granito Verde Ubatuba', 195, 175, 315, 19000, 'NF-005', CURRENT_TIMESTAMP, true, false);

-- Inserir processos de serragem
INSERT INTO processos_serragem (bloco_id, data_inicio, quantidade_chapas, observacoes)
SELECT 
    b.id,
    CASE 
        WHEN b.codigo = 'BL001' THEN CURRENT_TIMESTAMP - INTERVAL '30 days'
        WHEN b.codigo = 'BL002' THEN CURRENT_TIMESTAMP - INTERVAL '25 days'
        WHEN b.codigo = 'BL003' THEN CURRENT_TIMESTAMP - INTERVAL '20 days'
        WHEN b.codigo = 'BL004' THEN CURRENT_TIMESTAMP - INTERVAL '15 days'
        WHEN b.codigo = 'BL005' THEN CURRENT_TIMESTAMP - INTERVAL '10 days'
    END as data_inicio,
    CASE 
        WHEN b.codigo = 'BL001' THEN 20
        WHEN b.codigo = 'BL002' THEN 18
        WHEN b.codigo = 'BL003' THEN 22
        WHEN b.codigo = 'BL004' THEN 25
        WHEN b.codigo = 'BL005' THEN 19
    END as quantidade_chapas,
    CASE 
        WHEN b.codigo = 'BL001' THEN 'Processo de serragem padrão'
        WHEN b.codigo = 'BL002' THEN 'Serragem com espessura especial'
        WHEN b.codigo = 'BL003' THEN 'Serragem com acabamento polido'
        WHEN b.codigo = 'BL004' THEN 'Processo de serragem com reforço'
        WHEN b.codigo = 'BL005' THEN 'Serragem com espessura fina'
    END as observacoes
FROM blocos b
WHERE b.codigo IN ('BL001', 'BL002', 'BL003', 'BL004', 'BL005');

-- Inserir chapas
INSERT INTO chapas (bloco_id, tipo_material, comprimento, largura, espessura, valor_unitario, data_cadastro, disponivel, quantidade_estoque)
SELECT 
    b.id,
    b.tipo_material,
    CASE 
        WHEN b.codigo = 'BL001' THEN 300
        WHEN b.codigo = 'BL002' THEN 310
        WHEN b.codigo = 'BL003' THEN 290
        WHEN b.codigo = 'BL004' THEN 305
        WHEN b.codigo = 'BL005' THEN 315
    END as comprimento,
    CASE 
        WHEN b.codigo = 'BL001' THEN 180
        WHEN b.codigo = 'BL002' THEN 190
        WHEN b.codigo = 'BL003' THEN 175
        WHEN b.codigo = 'BL004' THEN 185
        WHEN b.codigo = 'BL005' THEN 195
    END as largura,
    CASE 
        WHEN b.codigo = 'BL001' THEN 2.0
        WHEN b.codigo = 'BL002' THEN 2.5
        WHEN b.codigo = 'BL003' THEN 3.0
        WHEN b.codigo = 'BL004' THEN 2.0
        WHEN b.codigo = 'BL005' THEN 2.5
    END as espessura,
    CASE 
        WHEN b.codigo = 'BL001' THEN 1200
        WHEN b.codigo = 'BL002' THEN 1500
        WHEN b.codigo = 'BL003' THEN 1300
        WHEN b.codigo = 'BL004' THEN 1400
        WHEN b.codigo = 'BL005' THEN 1600
    END as valor_unitario,
    CURRENT_TIMESTAMP as data_cadastro,
    true as disponivel,
    CASE 
        WHEN b.codigo = 'BL001' THEN 15
        WHEN b.codigo = 'BL002' THEN 12
        WHEN b.codigo = 'BL003' THEN 18
        WHEN b.codigo = 'BL004' THEN 20
        WHEN b.codigo = 'BL005' THEN 14
    END as quantidade_estoque
FROM blocos b
WHERE b.codigo IN ('BL001', 'BL002', 'BL003', 'BL004', 'BL005'); 